using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Personal.Infra.HealthCheck.Database
{
    public abstract class DbConnectionHealthCheck : IHealthCheck
    {
        protected readonly string ConnectionString;

        protected DbConnectionHealthCheck(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected abstract DbConnection CreateConnection(string connectionString);

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = CreateConnection(ConnectionString);
                await connection.OpenAsync(cancellationToken);
            }
            catch (DbException ex)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return HealthCheckResult.Healthy();
        }
    }
}