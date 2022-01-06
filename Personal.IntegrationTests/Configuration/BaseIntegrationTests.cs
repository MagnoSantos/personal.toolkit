using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Personal.IntegrationTests.Configuration.IoC;

namespace Personal.IntegrationTests.Configuration
{
    public abstract class BaseIntegrationTests<TDependency>
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly TDependency Dependency;

        protected BaseIntegrationTests()
        {
            ServiceProvider = CompositionRoot.BuildServiceProvider();
            Dependency = ServiceProvider.GetRequiredService<TDependency>();
        }

        protected T GetDependency<T>() => ServiceProvider.GetRequiredService<T>();
    }

    public static class CompositionRoot
    {
        public static IServiceProvider BuildServiceProvider()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.tests.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            return services
                    .AddLogging(configure => configure.AddConsole())
                    .AddSingleton<IConfiguration>(_ => configuration)
                    .ConfigureContainer(configuration)
                    .BuildServiceProvider();
        }
    }
}