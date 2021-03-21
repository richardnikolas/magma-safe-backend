using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Api.Models;
using MagmaSafe.Borders.Dtos.HealthCheck;
using MagmaSafe.Borders.UseCases;

namespace MagmaSafe.Api.Controllers
{
    [Route("api/healthcheck")]
    [ApiController]
    public class HealthCheckController : Controller
    {
        readonly IActionResultConverter actionResultConverter;
        readonly IHealthCheckUseCase _healthCheckUseCase;

        public HealthCheckController(IActionResultConverter actionResultConverter, IHealthCheckUseCase healthCheckUseCase)
        {
            _healthCheckUseCase = healthCheckUseCase;
            this.actionResultConverter = actionResultConverter;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(HealthCheckStatus))]
        [ProducesResponseType(503, Type = typeof(HealthCheckStatus))]
        public async Task<IActionResult> Get()
        {
            var response = await _healthCheckUseCase.Execute();
            return actionResultConverter.Convert(response);
        }

        [HttpGet("ping")]
        [ProducesResponseType(200, Type = typeof(HealthCheckStatus))]
        public IActionResult GetPong()
        {
            return Ok();
        }
    }
}
