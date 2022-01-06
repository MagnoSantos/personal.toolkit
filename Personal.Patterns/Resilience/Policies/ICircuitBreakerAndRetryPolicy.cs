using Polly;
using Polly.Wrap;

namespace Personal.Patterns.Resilience.Policies
{
    public interface ICircuitBreakerAndRetryPolicy
    {
        public AsyncPolicyWrap GetCurrentPolicy();
        public IAsyncPolicy WaitAndRetry();
        public IAsyncPolicy CircuitBreaker();
    }
}