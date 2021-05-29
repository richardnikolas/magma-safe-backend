using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Dtos.ServersOfUsers;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.UseCases.ServersOfUsers;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.ServersOfUser
{
    public class UpdateIsFavoriteUseCase : IUpdateIsFavoriteUseCase
    {
        private readonly IServersOfUserRepository serversOfUserRepository;

        public UpdateIsFavoriteUseCase(IServersOfUserRepository serversOfUserRepository)
        {
            this.serversOfUserRepository = serversOfUserRepository;
        }

        public async Task<UseCaseResponse<string>> Execute(UpdateIsFavoriteRequest request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                if (!Guid.TryParse(request.ServerId, out Guid resServer) && !Guid.TryParse(request.UserId, out Guid resUser))
                    return response.SetBadRequest($"Invalid Guid passed as parameter. Request values = '{request.ServerId}' or '{request.UserId}'");

                var result = await serversOfUserRepository.UpdateIsFavorite(request);

                if (result)
                    return response.SetSuccess("Updated isFavorite");

                else
                    return response.SetInternalServerError($"Error while updating ServersOfUsers with request = {request}");
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Error while updating ServersOfUsers \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
