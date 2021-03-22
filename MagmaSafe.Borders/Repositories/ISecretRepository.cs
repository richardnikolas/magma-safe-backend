using MagmaSafe.Borders.Entities;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface ISecretRepository
    {
        Task<Secret> GetById(string id);
    }
}
