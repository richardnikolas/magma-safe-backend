using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Api.Controllers
{
    [Route("api/secret")]
    [ApiController]
    public class SecretController : Controller
    {
        readonly IActionResultConverter actionResultConverter;
        readonly IGetSecretUseCase _getSecretUseCase;

        public SecretController(IActionResultConverter actionResultConverter, IGetSecretUseCase getSecretUseCase)
        {
            this.actionResultConverter = actionResultConverter;
            _getSecretUseCase = getSecretUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Secret))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _getSecretUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }
    }
}
