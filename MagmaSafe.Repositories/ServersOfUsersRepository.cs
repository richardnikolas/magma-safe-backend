using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.SQLStatements;

namespace MagmaSafe.Repositories
{
    public class ServersOfUsersRepository : IServersOfUserRepository
    {
        private readonly IRepositoryHelper helper;

        public ServersOfUsersRepository(IRepositoryHelper helper) 
        {
            this.helper = helper;
        }

        public async Task<bool> CreateServerOfUser(CreateServersOfUserRequest request)
        {
            var param = new DynamicParameters();

            param.Add("@UserId", request.UserId, DbType.String);
            param.Add("@ServerId", request.ServerId, DbType.String);
            param.Add("@IsFavorite", false, DbType.Boolean);

            await DBQuery(ServersOfUsersStatements.CREATE_SERVERS_OF_USER, param);

            return true;
        }

        public async Task<int> GetCountFromServersOfUsers(string where)
        {
            return await GetCountQuery(ServersOfUsersStatements.GET_SERVERS_OF_USERS_COUNT, null, where);
        }

        #region Private Methods

        private async Task<int> GetCountQuery(string sql, object param, string where = "")
        {
            var fullSql = sql + where;

            using (var connection = helper.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(fullSql, param);
            }
        }

        private async Task<string> DBQuery(string sql, object param, string where = "")
        {
            var fullSql = sql + where;

            using (var connection = helper.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<string>(fullSql, param);
            }
        }

        #endregion
    }
}
