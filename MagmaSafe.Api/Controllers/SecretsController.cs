using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Api.Controllers
{
    [Route("api/secrets")]
    [ApiController]
    public class SecretsController : Controller
    {
        readonly IActionResultConverter actionResultConverter;
        readonly IGetSecretUseCase _getSecretUseCase;
        readonly ICreateSecretUseCase _createSecretUseCase;

        public SecretsController(IActionResultConverter actionResultConverter, IGetSecretUseCase getSecretUseCase, ICreateSecretUseCase createSecretUseCase)
        {
            this.actionResultConverter = actionResultConverter;
            _getSecretUseCase = getSecretUseCase;
            _createSecretUseCase = createSecretUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Secret))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _getSecretUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Create(CreateSecretRequest request)
        {
            var response = await _createSecretUseCase.Execute(request);
            return actionResultConverter.Convert(response);
        }
    }
}
