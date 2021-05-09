using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.Server;
using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;
using System.Collections.Generic;

namespace MagmaSafe.UseCases.Server 
{
    public class GetServersByUserIdUseCase : IGetServersByUserIdUseCase 
    { 
        private readonly IServerRepository serverRepository;
        private readonly ISecretRepository secretRepository;
        private readonly IServersOfUserRepository serversOfUserRepository;

        public GetServersByUserIdUseCase(
            IServerRepository serverRepository,
            ISecretRepository secretRepository,
            IServersOfUserRepository serversOfUserRepository) 
        {
            this.serverRepository = serverRepository;
            this.secretRepository = secretRepository;
            this.serversOfUserRepository = serversOfUserRepository;
        }

        public async Task<UseCaseResponse<List<ServerDTO>>> Execute(string userId)
        {
            var response = new UseCaseResponse<List<ServerDTO>>();

            try 
            {
                List<ServerDTO> serverDTOs = new List<ServerDTO>();

                List<Borders.Entities.Server> servers = await serverRepository.GetServersByUserId(userId);                

                if (servers != null)
                {
                    foreach(Borders.Entities.Server server in servers)
                    {
                        var secretsCount = await secretRepository.GetCountFromSecret($"WHERE serverId = '{server.Id}'");
                        var usersCount = await serversOfUserRepository.GetCountFromServersOfUsers($"WHERE serverId = '{server.Id}'");

                        var newServerDto = new ServerDTO(server, false, secretsCount, usersCount);

                        serverDTOs.Add(newServerDto);
                    }
                }

                if (serverDTOs.Count > 0)
                    return response.SetSuccess(serverDTOs);

                else
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find servers with UserId = {userId}"));
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting servers from UserId: {userId} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
