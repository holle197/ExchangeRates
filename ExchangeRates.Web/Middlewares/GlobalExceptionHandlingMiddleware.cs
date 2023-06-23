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

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        problem.Status = context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problem.Type = "Server Error";
                        problem.Title = "Server Error";
                        problem.Detail = "Internal Server Error";
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                        break;

                }
            }

        }
    }
}

