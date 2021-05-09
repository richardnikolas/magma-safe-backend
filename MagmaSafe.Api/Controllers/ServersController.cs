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

        public ServersController(
            IActionResultConverter actionResultConverter, 
            IGetServerUseCase getServerUseCase, 
            ICreateServerUseCase createServerUseCase, 
            IGetServersByUserIdUseCase getServerByUserIdUseCase
        ) 
        {
            this.actionResultConverter = actionResultConverter;
            _getServerUseCase = getServerUseCase;
            _createServerUseCase = createServerUseCase;
            _getServersByUserIdUseCase = getServerByUserIdUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Server))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id) 
        {
            var response = await _getServerUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }

        [HttpGet("user/{UserId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Server))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetServersOfUsers(string userid) 
        {
            var response = await _getServersByUserIdUseCase.Execute(userid);
            return actionResultConverter.Convert(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Create(CreateServerRequest request)
        {
            var response = await _createServerUseCase.Execute(request);
            return actionResultConverter.Convert(response);
        }
    }
}