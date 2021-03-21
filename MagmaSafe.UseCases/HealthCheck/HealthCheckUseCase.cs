using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using MagmaSafe.Borders.Dtos.HealthCheck;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.UseCases;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.HealthCheck
{
    public class HealthCheckUseCase : IHealthCheckUseCase
    {
        private readonly IHealthCheckRepository healthCheckRepository;

        public HealthCheckUseCase(IHealthCheckRepository healthCheckRepository)
        {
            this.healthCheckRepository = healthCheckRepository;
        }

        public async Task<UseCaseResponse<HealthCheckStatus>> Execute()
        {
            var result = new UseCaseResponse<HealthCheckStatus>();

            try
            {
                var healthCheckStatus = new HealthCheckStatus();

                var dbHealthTask = healthCheckRepository.CheckDBConnection();

                healthCheckStatus.Activities.Add(new ActivitieHealthCheck
                {
                    Name = "DbConnection",
                    Success = await dbHealthTask
                });

                return (healthCheckStatus.Success) ? result.SetSuccess(healthCheckStatus) : result.SetUnavailable(healthCheckStatus);
            }
            catch (Exception e)
            {
                ErrorMessage errMsg = new ErrorMessage("00.09", "Unexpected error on health check");
                var errorData = e.Data?.OfType<DictionaryEntry>().ToDictionary(kv => kv.Key.ToString(), kv => kv.Value?.ToString());

                return result.SetInternalServerError(e.Message, new[] { errMsg });
            }
        }
    }
}
