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

        public async Task<User> GetByEmail(string email)
        {
            var param = new DynamicParameters();
            param.Add("@Email", email, DbType.String);

            var whereEmail = "WHERE email = @Email";

            return await GetUserQuery(UserStatements.GET_USER, param, whereEmail, null);
        }

        public async Task<string> CreateUser(CreateUserRequest request)
        {
            var param = new DynamicParameters();

            var newId = Guid.NewGuid().ToString();

            param.Add("@Id", newId, DbType.String);
            param.Add("@Name", request.Name, DbType.String);
            param.Add("@Email", request.Email, DbType.String);
            param.Add("@Password", securityHelper.MD5Hash(request.Password), DbType.String);
            param.Add("@IsAdmin", request.IsAdmin, DbType.Boolean);
            param.Add("@IsActive", true, DbType.Boolean);

            await DBUserQuery(UserStatements.CREATE_USER, param, null);

            return newId;
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
