using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.UseCases;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        readonly IActionResultConverter actionResultConverter;
        readonly IGetUserUseCase _getUserUseCase;

        public UserController(IActionResultConverter actionResultConverter, IGetUserUseCase getUserUseCase)
        {
            this.actionResultConverter = actionResultConverter;
            _getUserUseCase = getUserUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _getUserUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }
    }
}
