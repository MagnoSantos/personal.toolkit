using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Personal.Infra.Clients;
using Personal.Infra.HealthCheck;
using Personal.Infra.HealthCheck.Database.Sql;
using Personal.Patterns.Builder;
using Personal.Patterns.Resilience.Policies;
using Personal.Patterns.Resilience.Service;
using Personal.Patterns.Strategy;
using Personal.Projects.Worker._1___Domain.Abstractions.Repositories;
using Personal.Projects.Worker._2___Infra.Data.EntityFramework;
using Personal.Projects.Worker._2___Infra.Data.Repositories;
using System;

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

            //Strategy
            services.AddScoped<IStrategy, StrategyA>()
                    .AddScoped<IStrategy, StrategyB>();

            //Clients
            services.AddScoped<IAclClient, AclClient>();

            //Repositories
            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .EnableSensitiveDataLogging();
            })
                .AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}