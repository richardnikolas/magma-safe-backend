using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.SQLStatements;

namespace MagmaSafe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryHelper helper;

        public UserRepository(IRepositoryHelper helper)
        {
            this.helper = helper;
        }

        public async Task<User> GetById(string id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.String);

            var whereId = "WHERE id = @Id";

            return await GetUserQuery(UserStatements.GET_USER, param, whereId, null);
        }

        private async Task<User> GetUserQuery(string sql, object param, string where, string order = "")
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection())
            {
                return connection.QueryFirstOrDefault<User>(fullSql, param);
            }
        }
    }
}
