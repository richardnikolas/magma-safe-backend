using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.User;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<User> GetByEmail(string email);
        Task<string> CreateUser(CreateUserRequest request);
    }
}
