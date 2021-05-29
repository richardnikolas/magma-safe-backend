using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using System.Net;
using System.Threading.Tasks;
using MagmaSafe.Borders.Dtos.ServersOfUsers;
using MagmaSafe.Borders.UseCases.Server;
using MagmaSafe.Borders.UseCases.ServersOfUsers;
using MagmaSafe.Shared.Models;
using MagmaSafe.Borders.Dtos.User;

namespace MagmaSafe.Api.Controllers 
{
    [Route("api/serversofusers")]
    [ApiController]
    public class ServersOfUsersController
    {
        readonly IActionResultConverter actionResultConverter;
        readonly ICreateServersOfUsersUseCase _createServersOfUsersUseCase;
        readonly IUpdateIsFavoriteUseCase _updateIsFavorite;

        public ServersOfUsersController(
            IActionResultConverter actionResultConverter,
            ICreateServersOfUsersUseCase createServersOfUsersUseCase,
            IUpdateIsFavoriteUseCase updateIsFavorite
        )
        {
            this.actionResultConverter = actionResultConverter;
            _createServersOfUsersUseCase = createServersOfUsersUseCase;
            _updateIsFavorite = updateIsFavorite;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> CreateByUserEmail(AddUserToServerDTO request)
        {
            var response = await _createServersOfUsersUseCase.Execute(request);
            return actionResultConverter.Convert(response);
        }

        [HttpPatch("isFavorite")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> UpdateIsFavorite(UpdateIsFavoriteRequest request)
        {
            var response = await _updateIsFavorite.Execute(request);
            return actionResultConverter.Convert(response);
        }
    }
}