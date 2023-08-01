using ExchangeRates.Core.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExchangeRates.Web.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                ProblemDetails problem = new();
                switch (e)
                {
                    //in future, add specific exception with specific ProblemDetails properies
                    case ConversingException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        problem.Status = context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        problem.Type = "Invalid Paramether";
                        problem.Title = "Invalid Conversing Paramether";
                        problem.Detail = e.Message;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                        break;
                    case FetcherExceptions:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        problem.Status = context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problem.Type = "Fetcher Error";
                        problem.Title = "Cannot Fetch Data.";
                        problem.Detail = e.Message;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        problem.Status = context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problem.Type = "Internal Server Error";
                        problem.Title = "Internal Server Error";
                        problem.Detail = "Internal Server Error";
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                        break;

                }
            }

        }
    }
}

