using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using BillsOfExchange.Exceptions;
using BillsOfExchange.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace BillsOfExchange.Middlewares
{
    /// <summary>
    /// Middleware pro zpracování exceptions
    /// </summary>
    public class ExceptionProblemDetailHandler
    {
        /// <summary>
        /// Middleware invoke
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = errorFeature.Error;


            switch (exception)
            {
                case null:
                {
                    return Task.CompletedTask;
                }
                case OperationCanceledException e:
                {
                    Log.Information($"API call cancelled: {e.Message}");
                    return Task.CompletedTask;
                }
            }

            var httpStatus = HttpStatusCode.InternalServerError;

            var problemDetails = this.ProcessException(exception, ref httpStatus);

            problemDetails.Instance = $"urn:{context.Request.Path.Value?.Replace('/', ':')}:{httpStatus}:{context.TraceIdentifier}".ToLower().Replace("::", ":");
            problemDetails.Status = (int) httpStatus;
            problemDetails.Type = exception.GetType().FullName;


            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.WriteJson(problemDetails, "application/problem+json");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Zpracování chyby na ProblemDetail
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="httpStatus"></param>
        /// <returns></returns>
        protected virtual ProblemDetails ProcessException(Exception exception, ref HttpStatusCode httpStatus)
        {
            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case BadHttpRequestException e:
                {
                    httpStatus = (HttpStatusCode) typeof(BadHttpRequestException).GetProperty("StatusCode",
                        BindingFlags.NonPublic | BindingFlags.Instance).GetValue(e);

                    problemDetails.Title = "Došlo k chybě při ověření požadavku.";
                    problemDetails.Detail = e.Message;
                    Log.Warning($"API call exception: {e}");
                    break;
                }
                case NotFoundException e:
                {
                    httpStatus = HttpStatusCode.NotFound;
                    problemDetails.Title = "Nenalezeno.";
                    problemDetails.Detail = e.Message;
                    Log.Warning($"API call exception: {e}");
                    break;
                }
                default:
                {
                    problemDetails.Title = "Došlo k chybě na serveru.";
                    problemDetails.Detail = exception.Message;
                    Log.Warning($"API call exception: {exception}");
                    break;
                }
            }

            return problemDetails;
        }
    }
}