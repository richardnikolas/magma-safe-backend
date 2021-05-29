using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.UseCases.Server;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Api.Controllers 
{
    [Route("api/servers")]
    [ApiController]
    public class ServersController 
    {
        readonly IActionResultConverter actionResultConverter;
        readonly IGetServerUseCase _getServerUseCase;
        readonly ICreateServerUseCase _createServerUseCase; 
        readonly IGetServersByUserIdUseCase _getServersByUserIdUseCase;
        readonly ICreateServersOfUsersUseCase _createServersOfUsersUseCase;

        public ServersController(
            IActionResultConverter actionResultConverter, 
            IGetServerUseCase getServerUseCase, 
            ICreateServerUseCase createServerUseCase, 
            IGetServersByUserIdUseCase getServerByUserIdUseCase,
            ICreateServersOfUsersUseCase createServersOfUsersUseCase
        ) 
        {
            this.actionResultConverter = actionResultConverter;
            _getServerUseCase = getServerUseCase;
            _createServerUseCase = createServerUseCase;
            _getServersByUserIdUseCase = getServerByUserIdUseCase;
            _createServersOfUsersUseCase = createServersOfUsersUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Server))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id) 
        {
            var response = await _getServerUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Server))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetServersOfUsers(string userId) 
        {
            var response = await _getServersByUserIdUseCase.Execute(userId);
            return actionResultConverter.Convert(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Create(CreateServerRequest request)
        {
            var response = await _createServerUseCase.Execute(request);
            return actionResultConverter.Convert(response);
        }
    }
}