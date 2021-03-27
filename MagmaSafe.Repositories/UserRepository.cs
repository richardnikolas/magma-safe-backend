using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MagmaSafe.Borders.Entities;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Borders.Dtos.User;
using MagmaSafe.Repositories.SQLStatements;

namespace MagmaSafe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryHelper helper;
        private readonly ISecurityHelper securityHelper;

        public UserRepository(IRepositoryHelper helper, ISecurityHelper securityHelper)
        {
            this.helper = helper;
            this.securityHelper = securityHelper;
        }

        public async Task<User> GetById(string id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.String);

            var whereId = "WHERE id = @Id";

            return await GetUserQuery(UserStatements.GET_USER, param, whereId, null);
        }

        public async Task<string> CreateUser(CreateUserRequest request)
        {
            var param = new DynamicParameters();

            var newId = Guid.NewGuid().ToString();
            var encryptedPassword = securityHelper.MD5Hash(request.Password);

            param.Add("@Id", newId, DbType.String);
            param.Add("@Name", request.Name, DbType.String);
            param.Add("@Email", request.Email, DbType.String);
            param.Add("@Password", encryptedPassword, DbType.String);
            param.Add("@IsAdmin", request.IsAdmin, DbType.Boolean);
            param.Add("@IsActive", true, DbType.Boolean);

            await DBUserQuery(UserStatements.CREATE_USER, param, null);

            return newId;
        }

        public async Task<string> UpdateUserPassword(UpdateUserPasswordRequest request)
        {
            var param = new DynamicParameters();

            var newPassword = securityHelper.MD5Hash(request.NewPassword);

            param.Add("@Id", request.UserId, DbType.String);
            param.Add("@NewPassword", newPassword, DbType.String);

            await DBUserQuery(UserStatements.UPDATE_PASSWORD, param, null);

            return "Password atualizado com sucesso!";
        }

        #region Private Methods

        private async Task<User> GetUserQuery(string sql, object param, string where, string order = "")
        {
            var fullSql = sql + where + order;

            using (var connection = helper.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(fullSql, param);
            }
        }

        private async Task<string> DBUserQuery(string sql, object param, string where)
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
