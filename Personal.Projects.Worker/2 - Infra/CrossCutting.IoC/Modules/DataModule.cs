using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Personal.Projects.Worker._1___Domain.Abstractions.Repositories;
using Personal.Projects.Worker._2___Infra.Data.EntityFramework;
using Personal.Projects.Worker._2___Infra.Data.Repositories;
using System;

namespace Personal.Projects.Worker._2___Infra.CrossCutting.IoC.Modules
{
    public static class DataModule
    {
        private const string ConnectionStringName = "Database";
        /// <summary>
        /// Register dependencies to data module
        /// </summary>
        /// <param name="services"></param>
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies()
                       .UseSqlServer(configuration.GetConnectionString())
                       .LogTo(Console.WriteLine);
            })
                    .AddScoped<ICustomerRepository, CustomerRepository>();
        }

        private static string GetConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString(ConnectionStringName);
    }
}