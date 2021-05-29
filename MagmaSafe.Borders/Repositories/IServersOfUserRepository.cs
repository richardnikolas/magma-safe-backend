using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.Dtos.ServersOfUsers;
using MagmaSafe.Borders.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface IServersOfUserRepository
    {
        Task<int> GetCountFromServersOfUsers(string where);
        Task<bool> CreateServerOfUser(CreateServersOfUserRequest request);
        Task<string> GetServerIdFromServersOfUser(CreateServersOfUserRequest request);
        Task<List<ServersOfUser>> GetServersOfUsersByServerId(string serverId);
        Task<bool> UpdateIsFavorite(UpdateIsFavoriteRequest request);
    }
}
