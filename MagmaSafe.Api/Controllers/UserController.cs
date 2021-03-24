using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.UseCases.User;
using MagmaSafe.Borders.Dtos.User;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        readonly IActionResultConverter actionResultConverter;
        readonly IGetUserUseCase _getUserUseCase;
        readonly ICreateUserUseCase _createUserUseCase;

        public UserController(IActionResultConverter actionResultConverter, IGetUserUseCase getUserUseCase, ICreateUserUseCase createUserUseCase)
        {
            this.actionResultConverter = actionResultConverter;
            _getUserUseCase = getUserUseCase;
            _createUserUseCase = createUserUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _getUserUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var response = await _createUserUseCase.Execute(request);
            return actionResultConverter.Convert(response);
        }
    }
}
