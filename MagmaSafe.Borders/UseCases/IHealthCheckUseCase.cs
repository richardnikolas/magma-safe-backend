using MagmaSafe.Borders.Dtos.HealthCheck;
using MagmaSafe.Borders.Shared;

namespace MagmaSafe.Borders.UseCases
{
    public interface IHealthCheckUseCase : IUseCaseOnlyResponse<HealthCheckStatus>
    {
    }
}
