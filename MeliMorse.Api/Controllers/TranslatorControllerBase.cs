using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MeliMorse.Api.Models;

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
}
