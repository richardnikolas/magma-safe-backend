using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.SQLStatements;

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

            await DBServerQuery(SecretStatements.CREATE_SECRET, param, null);

            return newId;
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

        private async Task<string> DBServerQuery(string sql, object param, string where = "")
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
