using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.UseCases;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Api.Controllers {


    [Route("api/servers")]
    [ApiController]
    public class ServersController {


        readonly IActionResultConverter actionResultConverter;
        readonly IGetServerUseCase _getServerUseCase;

        public ServersController(IActionResultConverter actionResultConverter, IGetServerUseCase getServerUseCase) {
            this.actionResultConverter = actionResultConverter;
            _getServerUseCase = getServerUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Server))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id) {
            var response = await _getServerUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }



    }
}
