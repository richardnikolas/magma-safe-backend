using System;
using System.Threading.Tasks;
using System.Linq;
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
        private readonly IUserRepository userRepository;
        private readonly IServersOfUserRepository serversOfUserRepository;        

        public GetServersByUserIdUseCase(
            IServerRepository serverRepository,
            ISecretRepository secretRepository,
            IUserRepository userRepository,
            IServersOfUserRepository serversOfUserRepository
            ) 
        {
            this.serverRepository = serverRepository;
            this.secretRepository = secretRepository;
            this.userRepository = userRepository;
            this.serversOfUserRepository = serversOfUserRepository;
        }

        public async Task<UseCaseResponse<List<ServerDTO>>> Execute(string userId)
        {
            var response = new UseCaseResponse<List<ServerDTO>>();

            try 
            {
                if (!Guid.TryParse(userId, out Guid result))
                    return response.SetBadRequest($"Invalid Guid passed as parameter. Request value = '{userId}'");

                List<ServerDTO> serverDTOs = new List<ServerDTO>();

                List<Borders.Entities.Server> servers = await serverRepository.GetServersByUserId(userId);                

                if (servers != null && servers.Count > 0)
                {
                    foreach(Borders.Entities.Server server in servers)
                    {
                        var secretsCount = await secretRepository.GetCountFromSecret($"WHERE serverId = '{server.Id}'");
                        var serversOfUsers = await serversOfUserRepository.GetServersOfUsersByServerId($"WHERE serverId = '{server.Id}'");
                        var admin = await userRepository.GetById(server.AdminId);
                        var isFavorite = serversOfUsers.SingleOrDefault(s => (s.ServerId == server.Id && s.UserId == userId))?.IsFavorite ?? false;

                        var newServerDto = new ServerDTO(server, isFavorite, secretsCount, serversOfUsers.Count, admin.Name);

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
