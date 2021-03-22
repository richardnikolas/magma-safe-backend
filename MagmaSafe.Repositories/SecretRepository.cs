using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.SQLStatements;

namespace MagmaSafe.Repositories
{
    public class SecretRepository : ISecretRepository
    {
        private readonly IRepositoryHelper helper;

        public SecretRepository(IRepositoryHelper helper)
        {
            this.helper = helper;
        }

        public async Task<Secret> GetById(string id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.String);

            var whereId = "WHERE id = @Id";

            return await GetUserQuery(SecretStatements.GET_SECRET, param, whereId, null);
        }

        private async Task<Secret> GetUserQuery(string sql, object param, string where, string order = "")
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection())
            {
                return connection.QueryFirstOrDefault<Secret>(fullSql, param);
            }
        }
    }
}
