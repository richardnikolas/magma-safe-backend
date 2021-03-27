using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Secret;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface ISecretRepository
    {
        Task<Secret> GetById(string id);
        Task<string> CreateSecret(CreateSecretRequest request);
    }
}
