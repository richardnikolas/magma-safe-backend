using System.Data;
using System.Data.Common;
using System.Linq;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Repositories.Factories;
using MagmaSafe.Shared.Configurations;

namespace MagmaSafe.Repositories.Helpers
{
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly DbProviderFactory dbProviderFactory;
        private readonly string connectionString;

        public RepositoryHelper(ApplicationConfig configuration)
        {
            dbProviderFactory = DatabaseFactory.GetDbProviderFactory(configuration.Database.DbFactoryName, configuration.Database.AssemblyName);
            connectionString = configuration.Database.ConnectionString;
        }

        public IDbConnection GetConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = connectionString;

            return connection;
        }

        private string[] GetErrorValues(string errorMessage)
        {
            var errorMessageSplit = errorMessage.Split('(', ')');
            var valuesSplit = errorMessageSplit[3].Split(',');

            return valuesSplit.Select(value => value.Trim()).ToArray();
        }
    }
}
