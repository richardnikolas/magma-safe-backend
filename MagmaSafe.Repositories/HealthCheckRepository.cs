using Dapper;
using MySqlConnector;
using System.Threading.Tasks;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;

namespace MagmaSafe.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly IRepositoryHelper helper;

        public HealthCheckRepository(IRepositoryHelper helper)
        {
            this.helper = helper;
        }

        public async Task<bool> CheckDBConnection()
        {
            using (var connection = helper.GetConnection())
            {
                try
                {
                    return await connection.QueryFirstOrDefaultAsync<bool>("SELECT 1");
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
