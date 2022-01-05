using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Personal.Infra.HealthCheck.Database.Sql;

namespace Personal.IntegrationTests.Configuration.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            //Add dependency injection services
            services.AddHealthChecks()
                    .AddCheck<SqlServerConnectionHealthCheck>("sql_server_health_check");

            return services;
        }
    }
}