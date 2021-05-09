using MagmaSafe.Borders.Dtos.Server;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface IServersOfUserRepository
    {
        Task<int> GetCountFromServersOfUsers(string where);
        Task<bool> CreateServerOfUser(CreateServersOfUserRequest request);
    }
}
