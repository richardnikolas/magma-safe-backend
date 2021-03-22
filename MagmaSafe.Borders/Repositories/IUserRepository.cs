using System;
using MagmaSafe.Borders.Entities;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
    }
}
