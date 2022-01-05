using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Personal.Infra.HealthCheck.Database.Sql;
using Personal.IntegrationTests.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace Personal.IntegrationTests.HealthCkeck
{
    public class SqlServerConnectionHealthCheckTests : BaseIntegrationTests<SqlServerConnectionHealthCheck>
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        private SqlServerConnectionHealthCheck _sqlServerConnectionHealthCheck;

        public SqlServerConnectionHealthCheckTests()
        {
            _configuration = GetDependency<IConfiguration>();
            _connectionString = _configuration.GetConnectionString("Database");
        }

        [Fact]
        public async Task CheckHealthAsync_Expecting_Healthy()
        {
            _sqlServerConnectionHealthCheck = new(_connectionString);

            var result = await _sqlServerConnectionHealthCheck.CheckHealthAsync(new HealthCheckContext(), default);

            result.Status.Should().Be(HealthStatus.Healthy);
        }
    }
}