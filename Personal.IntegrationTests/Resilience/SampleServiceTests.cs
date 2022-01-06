using Personal.IntegrationTests.Configuration;
using Personal.Patterns.Resilience.Policies;
using Personal.Patterns.Resilience.Service;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Personal.IntegrationTests.Resilience
{
    public class SampleServiceTests : BaseIntegrationTests<ISampleService>
    {
        private readonly ICircuitBreakerAndRetryPolicy _circuitBreakerAndRetryPolicy;

        public SampleServiceTests()
        {
            _circuitBreakerAndRetryPolicy = GetDependency<ICircuitBreakerAndRetryPolicy>();
        }

        [Fact]
        public void GetCurrentPolicy_Expecting_RetryAndCircuitBreakerExecution()
        {
            var policies = _circuitBreakerAndRetryPolicy.GetCurrentPolicy();

            Assert.ThrowsAsync<NotImplementedException>(
                async () => await policies.ExecuteAsync(async () => await Dependency.AnyMethod())
            );
        }
    }
}