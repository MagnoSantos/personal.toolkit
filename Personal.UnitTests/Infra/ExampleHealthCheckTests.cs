using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Personal.Infra.HealthCheck;
using System.Threading.Tasks;
using Xunit;

namespace Personal.UnitTests.Infra
{
    public class ExampleHealthCheckTests
    {
        [Fact]
        public async Task CheckHealthAsync_Expecting_Healthy()
        {
            var health = new ExampleHealthCheck(true);

            var result = await health.CheckHealthAsync(new HealthCheckContext(), default);

            result.Status.Should().Be(HealthStatus.Healthy);
        }
    }
}