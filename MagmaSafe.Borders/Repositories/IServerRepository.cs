using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Server;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories 
{
    public interface IServerRepository 
    {
        Task<Server> GetById(string id);
        Task<string> CreateServer(CreateServerRequest request);
    }
}
