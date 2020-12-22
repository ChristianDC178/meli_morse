using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MeliMorse.Translator;
using Microsoft.AspNetCore.Http;

namespace MeliMorse.Api.Controllers
{

    public class TranslatorControllerBase : ControllerBase
    {


        [NonAction]
        public IActionResult ExecuteResponse(Func<MorseResponse> func)
        {
            try
            {
                MorseResponse responseApi = func.Invoke();
                return StatusCode(responseApi.Code, responseApi);
            }
            catch (Exception ex)
            {

                //La ex puede ser logueada

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new MorseResponse()
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Response = "Unespected Error"
                    });
            }
        }


    }


    [ApiController]
    [Route("api/[controller]")]
    public class TranslatorController : TranslatorControllerBase
    {

        private readonly ILogger<TranslatorController> _logger;

        public TranslatorController(ILogger<TranslatorController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("2text")]
        public IActionResult TranslateToText(MorseRequest request)
        {
            return ExecuteResponse(() =>
            {

                MorseResponse morseResponse = new MorseResponse();

                morseResponse.Code = StatusCodes.Status200OK;
                morseResponse.Response = MorseDecoder.translate2Human(request.Text);

                return morseResponse;

            });
        }

        [HttpPost]
        [Route("2morse")]
        public IActionResult TranslateToMorse(MorseRequest request)
        {
            return ExecuteResponse(() =>
            {

                MorseResponse morseResponse = new MorseResponse();

                morseResponse.Code = StatusCodes.Status200OK;
                morseResponse.Response = MorseDecoder.traslate2Morse(request.Text);

                return morseResponse;

            });
        }

    }

    public class MorseRequest
    {
        public string Text { get; set; }
    }

    public class MorseResponse
    {
        public int Code { get; set; }
        public string Response { get; set; }
    }
}
