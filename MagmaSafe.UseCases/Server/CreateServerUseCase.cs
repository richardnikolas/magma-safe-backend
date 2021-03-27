using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.UseCases.Server;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.Server
{
    public class CreateServerUseCase : ICreateServerUseCase
    {
        private readonly IServerRepository serverRepository;
        private readonly IUserRepository userRepository;

        public CreateServerUseCase(IServerRepository serverRepository, IUserRepository userRepository)
        {
            this.serverRepository = serverRepository;
            this.userRepository = userRepository;
        }

        public async Task<UseCaseResponse<string>> Execute(CreateServerRequest request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                var admin = await userRepository.GetById(request.AdminId);

                if (admin == null || !admin.IsAdmin)
                    return response.SetBadRequest($"Unable to find admin with id = {request.AdminId}");

                var serverId = await serverRepository.CreateServer(request);

                if (serverId != null)
                    return response.SetSuccess(serverId);
                
                else                
                    return response.SetInternalServerError($"Unable to create server with request = {request}");
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error creating server \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
