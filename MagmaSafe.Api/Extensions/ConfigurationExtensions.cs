using Microsoft.Extensions.Configuration;
using MagmaSafe.Shared.Configurations;

namespace MagmaSafe.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source)
        {
            var applicationConfig = source.Get<ApplicationConfig>();

            applicationConfig.Database.ConnectionString = source.GetValue<string>("Database:ConnectionString");
            applicationConfig.Database.AssemblyName = source.GetValue<string>("Database:AssemblyName");
            applicationConfig.Database.DbFactoryName = source.GetValue<string>("Database:DbFactoryName");
            applicationConfig.Database.DatabaseName = source.GetValue<string>("Database:DatabaseName");
            applicationConfig.Database.DBUsername = source.GetValue<string>("Database:DBUsername");
            applicationConfig.Database.DBPassword = source.GetValue<string>("Database:DBPassword");
            applicationConfig.CorsOrigins = source.GetSection("CorsOrigins").Get<string[]>();

            return applicationConfig;
        }
    }
}
