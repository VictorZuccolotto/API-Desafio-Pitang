using DesafioPitang.WebApi.Configuration;
using DesafioPitang.WebApi.Middleware;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using tusdotnet.Helpers;

namespace DesafioPitang.WebApi
{
    public class Startup
    {
        public IConfiguration Configuracao { get; }
        public Startup(IConfiguration configuracao)
        {
            Configuracao = configuracao;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDependencyInjectionConfiguration();
            services.AddDatabaseConfiguration(Configuracao);
            services.AddFluentConfiguration();

            services.AddCors(o => o.AddPolicy("CORS_POLICY", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyOrigin()
                       //.AllowCredentials()
                       .AllowAnyMethod()
                       .WithExposedHeaders(CorsHelper.GetExposedHeaders());
            }));


            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(TimeSpan), () => new() { Type = "string", Example = new OpenApiString("00:00:00") });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio Pitang",
                    Version = "v1",
                    Description = "Agendamento para vacina do COVID-19"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Pitang v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("CORS_POLICY");

            app.UseMiddleware<ApiMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
