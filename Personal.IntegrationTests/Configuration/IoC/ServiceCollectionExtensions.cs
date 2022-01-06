using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Personal.Infra.HealthCheck;
using Personal.Infra.HealthCheck.Database.Sql;
using Personal.Patterns.Builder;
using Personal.Patterns.Resilience.Policies;
using Personal.Patterns.Resilience.Service;

namespace Personal.IntegrationTests.Configuration.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            //Add dependency injection services
            services.AddHealthChecks()
                    .AddCheck<SqlServerConnectionHealthCheck>("sql_server_health_check")
                    .AddCheck<ExampleHealthCheck>("example_health_check");

            //Builder
            services.AddScoped<ISampleBuilder, SampleBuilder>();

            //Resilience
            services.AddScoped<ICircuitBreakerAndRetryPolicy, CircuitBreakerAndRetryPolicy>()
                    .AddScoped<ISampleService, SampleService>();

            return services;
        }
    }
}