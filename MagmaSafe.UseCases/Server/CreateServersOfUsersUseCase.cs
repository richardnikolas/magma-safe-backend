using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.Dtos.User;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.UseCases.Server;
using MagmaSafe.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagmaSafe.UseCases.Server
{
    public class CreateServersOfUsersUseCase : ICreateServersOfUsersUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IServerRepository serverRepository;
        private readonly IServersOfUserRepository serversOfUserRepository;

        public CreateServersOfUsersUseCase(
            IUserRepository userRepository,
            IServerRepository serverRepository,
            IServersOfUserRepository serversOfUserRepository)
        {
            this.userRepository = userRepository;
            this.serverRepository = serverRepository;
            this.serversOfUserRepository = serversOfUserRepository;
        }

        public async Task<UseCaseResponse<string>> Execute(AddUserToServerDTO request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                var user = await userRepository.GetByEmail(request.UserEmail);

                if (user == null)
                    return response.SetBadRequest($"Unable to find user with email = {request.UserEmail}");

                var server = await serverRepository.GetById(request.ServerId);

                if (server == null)
                    return response.SetBadRequest($"Unable to find server with serverId = {request.ServerId}");

                CreateServersOfUserRequest createServersOfUserRequest = new CreateServersOfUserRequest(user.Id, server.Id);

                var serversOfUsers = await serversOfUserRepository.GetServerIdFromServersOfUser(createServersOfUserRequest);

                if (serversOfUsers != null)
                    return response.SetBadRequest($"This user already is added to server = {request.ServerId}");
                                
                var addUserToServerOfUser = await serversOfUserRepository.CreateServerOfUser(createServersOfUserRequest);

                if (addUserToServerOfUser)
                    return response.SetSuccess(request.ServerId);
                else
                    return response.SetInternalServerError($"Unable to create serversofusers with request = {request.ServerId}");
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error creating serversofusers \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
