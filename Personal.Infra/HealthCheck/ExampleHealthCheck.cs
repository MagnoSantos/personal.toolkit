using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Personal.Infra.HealthCheck
{
    public class ExampleHealthCheck : IHealthCheck
    {
        private readonly bool _healthCheckResultHealthy;

        public ExampleHealthCheck(bool healthCheckResultHealthy)
        {
            _healthCheckResultHealthy = healthCheckResultHealthy;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (_healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
            }

            return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "An unhealthy result."));
        }
    }
}