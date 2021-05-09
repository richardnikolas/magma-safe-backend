using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Dtos.Server;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.SQLStatements;

namespace MagmaSafe.Repositories 
{
    public class ServerRepository : IServerRepository 
    {
        private readonly IRepositoryHelper helper;

        public ServerRepository(IRepositoryHelper helper) 
        {
            this.helper = helper;
        }

        public async Task<Server> GetById(string id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.String);

            var whereId = "WHERE id = @Id";
            return await GetServerQuery(ServerStatements.GET_SERVER, param, whereId, null);
        }

        public async Task<List<Server>> GetServersByUserId(string userId) 
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId, DbType.String);

            var whereId = "LEFT JOIN serversofusers ON (serversofusers.serverId = server.id) WHERE userId = @UserId";
            return await GetServerByUserIdQuery(ServerStatements.GET_SERVER, param, whereId, null);
        }

        public async Task<int> GetCountFromServer(string where)
        {
            return await GetCountQuery(ServerStatements.GET_SERVER_COUNT, null, where);
        }

        public async Task<string> CreateServer(CreateServerRequest request)
        {
            var param = new DynamicParameters();

            var newId = Guid.NewGuid().ToString();

            param.Add("@Id", newId, DbType.String);
            param.Add("@Name", request.Name, DbType.String);
            param.Add("@AdminId", request.AdminId, DbType.String);
            param.Add("@CreatedAt", DateTime.Now, DbType.DateTime);
            param.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);

            await DBQuery(ServerStatements.CREATE_SERVER, param);

            return newId;
        }

        #region Private Methods

        private async Task<Server> GetServerQuery(string sql, object param, string where = "", string order = "")
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection()) 
            {
                return await connection.QueryFirstOrDefaultAsync<Server>(fullSql, param);
            }
        }

        private async Task<List<Server>> GetServerByUserIdQuery(string sql, object param, string where , string order = "") 
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection()) 
            {
                return await connection.QueryAsync<Server>(fullSql, param) as List<Server>; 
            }
        }

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