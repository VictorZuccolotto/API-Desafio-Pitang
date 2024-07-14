using DesafioPitang.WebApi;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using System.Reflection;

public static class Program
{
    private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

    public static void Main(string[] args)
    {
        try
        {
            var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            _log.Info("Iniciando API");
            var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
            webHost.Build().Run();
        }
        catch (Exception ex)
        {
            _log.Error("Erro Fatal", ex);
            throw;
        }
    }
}

