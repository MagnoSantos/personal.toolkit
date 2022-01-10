using Microsoft.Extensions.DependencyInjection;

namespace Personal.Projects.Worker._2___Infra.CrossCutting.IoC.Modules
{
    public static class IoC
    {
        /// <summary>
        /// Configure container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureContainer(this IServiceCollection services)
        {
            DataModule.Register(services);
            DomainModule.Register(services);
            InfrastructureModule.Register(services);
            return services;
        }
    }
}