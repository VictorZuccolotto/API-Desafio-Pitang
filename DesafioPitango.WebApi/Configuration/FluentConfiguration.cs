using DesafioPitang.Validators.Fluent;
using FluentValidation.AspNetCore;

namespace DesafioPitang.WebApi.Configuration
{
    public static class FluentConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<CadastroAgendamentoValidator>());
        }
    }
}
