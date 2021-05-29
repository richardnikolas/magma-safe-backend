using System;
using System.Linq;
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
                if (Constants.ForbiddenWords.Any(request.Name.ToLower().Contains))
                {
                    return response.SetInternalServerError($"You tried to create an artifact with one or more forbidden words.");
                }

                var admin = await userRepository.GetById(request.AdminId);

                if (admin == null)
                    return response.SetBadRequest($"Unable to find admin with id = {request.AdminId}");

                if (!admin.IsAdmin)
                    return response.SetUnauthorizedError($"Only admins can create Servers. Id = {request.AdminId}");

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
