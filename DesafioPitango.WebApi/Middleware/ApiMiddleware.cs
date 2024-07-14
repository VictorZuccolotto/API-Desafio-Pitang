using DesafioPitang.Utils.Messages;
using DesafioPitang.Utils.Responses;
using log4net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace DesafioPitang.WebApi.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
                Stopwatch stopwatch = new();
                stopwatch.Start();
            try
            {
                stopwatch.Stop();
                _log.InfoFormat("Serviço executado com sucesso {0}, {1}, [{2} ms]", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
                await next(context);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                await HandleException(context, ex);
                _log.ErrorFormat("Erro no serviço [{0}] {1}, ({2} ms)\n[\n{3}\n]", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var messages = new List<string>();
            HttpStatusCode statusCode;
            switch (ex)
            {
                default:
                    messages.Add(InfraMessages.InternalServerError);
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            response.StatusCode = (int)statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(statusCode, messages)));
        }
    }
}
