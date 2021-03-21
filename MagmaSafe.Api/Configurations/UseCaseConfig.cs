using Microsoft.Extensions.DependencyInjection;
using MagmaSafe.UseCases.HealthCheck;
using MagmaSafe.Shared.Util;
using System;
using System.Linq;

namespace MagmaSafe.Api.Configurations
{
    public static class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Type classRefType = typeof(HealthCheckUseCase);
            //Here we want to get First or throw an InvalidOperationException, not FirstOrDefault.
            Type interfaceRefType = classRefType.GetInterfaces().First(it => it.Name == "I" + classRefType.Name);

            ReflectionUtils.ForEachInterfaceClass(interfaceRefType, classRefType, delegate (Type it, Type ct) {
                services.AddSingleton(it, ct);
            }, true, true);
        }
    }
}
