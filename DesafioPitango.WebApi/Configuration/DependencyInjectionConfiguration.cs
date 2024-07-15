using DesafioPitang.Repository.Interface;
using DesafioPitang.Repository;
using DesafioPitang.WebApi.Middleware;

namespace DesafioPitang.WebApi.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            InjectServices(services);
            InjectRepositories(services);
            InjectMidddlewares(services);

            services.AddScoped<ITransactionManager, TransactionManager>();
        }

        private static void InjectRepositories(IServiceCollection services)
        {

        }

        private static void InjectServices(IServiceCollection services)
        {

        }

        private static void InjectMidddlewares(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
        }
    }
}
