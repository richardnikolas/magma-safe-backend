using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Server;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MagmaSafe.Borders.Repositories 
{
    public interface IServerRepository 
    {
        Task<Server> GetById(string id);
        Task<List<Server>> GetServersByUserId(string userid);
        Task<int> GetCountFromServer(string where);
        Task<string> CreateServer(CreateServerRequest request);
    }
}
