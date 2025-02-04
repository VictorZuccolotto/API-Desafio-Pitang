﻿using DesafioPitang.Repository.Interface;
using DesafioPitang.Utils.Attributes;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using DesafioPitang.Utils.Responses;
using log4net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace DesafioPitang.WebApi.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly ITransactionManager _transactionManager;

        public ApiMiddleware(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var transacaoObrigatoria = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<Transaction>();
            try
            {
                if (transacaoObrigatoria != null)
                {
                    await _transactionManager.BeginTransactionAsync(transacaoObrigatoria.IsolationLevel);

                    await next.Invoke(context);

                    await _transactionManager.CommitTransactionsAsync();
                }
                else
                {
                    await next.Invoke(context);
                }
                stopwatch.Stop();
                _log.InfoFormat("Serviço executado com sucesso {0}, {1}, [{2} ms]", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                if (transacaoObrigatoria != null)
                    await _transactionManager.RollbackTransactionsAsync();
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
                case BadRequestException:
                    messages.Add(ex.Message);
                    statusCode = HttpStatusCode.BadRequest;
                    break;
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
