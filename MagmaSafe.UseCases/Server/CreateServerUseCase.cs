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
        private readonly IServersOfUserRepository serversOfUserRepository;
        private readonly IUserRepository userRepository;

        public CreateServerUseCase(
            IServerRepository serverRepository, 
            IServersOfUserRepository serversOfUserRepository, 
            IUserRepository userRepository
        )
        {
            this.serverRepository = serverRepository;
            this.serversOfUserRepository = serversOfUserRepository;
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

                var serverOfUser = false;

                if (serverId != null)
                {
                    CreateServersOfUserRequest createServersOfUserRequest = new CreateServersOfUserRequest(
                        userId: admin.Id, serverId: serverId
                    );

                    serverOfUser = await serversOfUserRepository.CreateServerOfUser(createServersOfUserRequest);
                }

                if (serverOfUser)
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
