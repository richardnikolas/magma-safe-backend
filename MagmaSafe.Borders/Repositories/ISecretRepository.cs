using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Secret;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MagmaSafe.Borders.Repositories
{
    public interface ISecretRepository
    {
        Task<Secret> GetById(string id);
        Task<List<Secret>> GetSecretsByServerId(string id);
        Task<int> GetCountFromSecret(string where);
        Task<string> CreateSecret(CreateSecretRequest request);
        Task<List<Secret>> GetSecretsByUserId(string id);
        Task<bool> UpdateLastAccessedByUser(UpdateLastAccessedByUserRequest request);
    }
}
