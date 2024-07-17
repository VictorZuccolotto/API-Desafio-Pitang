using DesafioPitang.Repository.Interface;
using DesafioPitang.Repository;
using DesafioPitang.WebApi.Middleware;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.Business.Interface.IBusiness;
using DesafioPitang.Business.Business;

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
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        }

        private static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IPacienteBusiness, PacienteBusiness>();
            services.AddScoped<IAgendamentoBusiness, AgendamentoBusiness>();

        }

        private static void InjectMidddlewares(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
        }
    }
}
