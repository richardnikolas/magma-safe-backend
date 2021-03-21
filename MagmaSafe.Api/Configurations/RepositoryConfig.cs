using Microsoft.Extensions.DependencyInjection;
using MagmaSafe.Shared.Util;
using MagmaSafe.Shared.Configurations;
using MagmaSafe.Repositories;
using MagmaSafe.Borders.Repositories;
using System;
using System.Linq;

namespace MagmaSafe.Api.Configurations
{
    public static class RepositoryConfig
    {
        public static void ConfigureServices(IServiceCollection services, ApplicationConfig applicationConfig)
        {
            Type classRefType = typeof(HealthCheckRepository);
            //Here we want to get First or throw an InvalidOperationException, not FirstOrDefault.
            Type interfaceRefType = classRefType.GetInterfaces().First(it => it.Name == "I" + classRefType.Name);

            ReflectionUtils.ForEachInterfaceClass(interfaceRefType, classRefType, delegate (Type it, Type ct)
            {
                services.AddSingleton(it, ct);
            }, true, true);
        }
    }
}
