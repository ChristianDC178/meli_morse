using MeliMorse.Translator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MeliMorse.Api.Models;

namespace MeliMorse.Api.Controllers
{

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

                morseResponse.Response = MorseDecoder.translate2Human(request.Text);
                morseResponse.Code = StatusCodes.Status200OK;

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
                
                morseResponse.Response = MorseDecoder.traslate2Morse(request.Text);
                morseResponse.Code = StatusCodes.Status200OK;

                return morseResponse;

            });
        }

    }

  
}
