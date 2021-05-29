using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.UseCases.Secret;
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
        readonly IGetSecretsByServerUseCase _getSecretsByServerUseCase;
        readonly IGetSecretsByUserIdUseCase _getSecretsByUserIdUseCase;
        readonly IUpdateLastAccessedByUserUseCase _updateLastAccessedByUserUseCase;

        public SecretsController(
            IActionResultConverter actionResultConverter,
            IGetSecretUseCase getSecretUseCase,
            ICreateSecretUseCase createSecretUseCase,
            IGetSecretsByServerUseCase getSecretByServerUseCase,
            IGetSecretsByUserIdUseCase getSecretsByUserIdUseCase,
            IUpdateLastAccessedByUserUseCase updateLastAccessedByUserUseCase)
        {
            this.actionResultConverter = actionResultConverter;
            _getSecretUseCase = getSecretUseCase;
            _createSecretUseCase = createSecretUseCase;
            _getSecretsByServerUseCase = getSecretByServerUseCase;
            _getSecretsByUserIdUseCase = getSecretsByUserIdUseCase;
            _updateLastAccessedByUserUseCase = updateLastAccessedByUserUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Secret))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _getSecretUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }

        [HttpGet("server/{serverId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SecretResponseDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetSecretsByServer(string serverId)
        {
            var response = await _getSecretsByServerUseCase.Execute(serverId);
            return actionResultConverter.Convert(response);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SecretResponseDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetSecretByUserId(string userId) 
        {
            var response = await _getSecretsByUserIdUseCase.Execute(userId);
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

        [HttpPatch("lastAccessedByUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SecretResponseDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> UpdateLastUser(UpdateLastAccessedByUserRequest request)
        {
            var response = await _updateLastAccessedByUserUseCase.Execute(request);
            return actionResultConverter.Convert(response);
        }
    }
}
