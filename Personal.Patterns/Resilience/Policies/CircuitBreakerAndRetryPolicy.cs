using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;
using System;

namespace Personal.Patterns.Resilience.Policies
{
    public class CircuitBreakerAndRetryPolicy : ICircuitBreakerAndRetryPolicy
    {
        private readonly ILogger<CircuitBreakerAndRetryPolicy> _logger;

        public CircuitBreakerAndRetryPolicy(ILogger<CircuitBreakerAndRetryPolicy> logger)
        {
            _logger = logger;
        }

        public IAsyncPolicy CircuitBreaker()
        {
            return Policy
              .Handle<Exception>()
              .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: ResilienceConfiguration.ExceptionsAllowedBeforeBreaking,
                    durationOfBreak: TimeSpan.FromSeconds(ResilienceConfiguration.DurationOfBreakInSeconds),
                    onBreak: (ex, breakDelay) =>
                    {
                        _logger.LogInformation("Circuit breaker opened");
                        _logger.LogInformation($".Breaker logging: Breaking the circuit for {breakDelay.TotalMilliseconds} ms!");
                        _logger.LogInformation($"..due to: {ex.Message}");
                    },
                    onReset: () => _logger.LogInformation(".Breaker logging: Call ok! Closed the circuit again!"),
                    onHalfOpen: () => _logger.LogInformation(".Breaker logging: Half-open: Next call is a trial!")
                );
        }

        public IAsyncPolicy WaitAndRetry()
        {
            return Policy
               .Handle<Exception>()
               .WaitAndRetryAsync(
                ResilienceConfiguration.RetryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(ResilienceConfiguration.RetryPow, retryAttempt))
               , onRetry: (outcomeType, timespan, retryCount, context) =>
               {
                   _logger.LogWarning($"Trying for the {retryCount} time!");
                   _logger.LogWarning($"Retrying one more time for correlationId '{context.CorrelationId}' after {timespan}. Current retry count is {retryCount}");
               });
        }

        public AsyncPolicyWrap GetCurrentPolicy()
            => Policy.WrapAsync(WaitAndRetry(), CircuitBreaker());
    }
}