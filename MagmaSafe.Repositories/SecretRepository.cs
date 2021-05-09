using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.SQLStatements;
using System.Collections.Generic;

namespace MagmaSafe.Repositories
{
    public class SecretRepository : ISecretRepository
    {
        private readonly IRepositoryHelper helper;
        private readonly ISecurityHelper securityHelper;

        public SecretRepository(IRepositoryHelper helper, ISecurityHelper securityHelper)
        {
            this.helper = helper;
            this.securityHelper = securityHelper;
        }

        public async Task<Secret> GetById(string id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.String);

            var whereId = "WHERE id = @Id";

            return await GetSecretQuery(SecretStatements.GET_SECRET, param, whereId, null);
        }

        public async Task<int> GetCountFromSecret(string where)
        {
            return await GetCountQuery(SecretStatements.GET_SECRET_COUNT, null, where);
        }

        public async Task<string> CreateSecret(CreateSecretRequest request)
        {
            var param = new DynamicParameters();

            var newId = Guid.NewGuid().ToString();

            param.Add("@Id", newId, DbType.String);
            param.Add("@Name", request.Name, DbType.String);
            param.Add("@MagmaSecret", securityHelper.MD5Hash(request.MagmaSecret), DbType.String);
            param.Add("@UserId", request.UserId, DbType.String);
            param.Add("@ServerId", request.ServerId, DbType.String);
            param.Add("@CreatedAt", DateTime.Now, DbType.DateTime);
            param.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);
            param.Add("@LastAccessedByUser", request.UserId, DbType.String);
            param.Add("@LastAccessed", DateTime.Now, DbType.DateTime);

            await DBQuery(SecretStatements.CREATE_SECRET, param, null);

            return newId;
        }

        public async Task<List<Secret>> GetSecretsByServerId(string id)
        {
            var param = new DynamicParameters();
            param.Add("@ServerId", id, DbType.String);

            var whereId = "WHERE serverId = @serverId";

            return await GetSecretsByServerQuery(SecretStatements.GET_SECRET, param, whereId, null);
        }

        #region Private Methods

        private async Task<Secret> GetSecretQuery(string sql, object param, string where, string order = "")
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Secret>(fullSql, param);
            }
        }

        private async Task<List<Secret>> GetSecretsByServerQuery(string sql, object param, string where, string order = "")
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection())
            {
                return await connection.QueryAsync<Secret>(fullSql, param) as List<Secret>;
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

        private async Task<int> GetCountQuery(string sql, object param, string where = "")
        {
            var fullSql = sql + where;

            using (var connection = helper.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(fullSql, param);
            }
        }

        #endregion
    }
}
