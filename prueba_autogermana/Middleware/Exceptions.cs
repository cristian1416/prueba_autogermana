
using Autogermana.Api.Responses;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;

namespace Autogermana.Api.Middleware
{
    public class Exceptions : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                sw.Stop();

                var (code, msg) = MapException(ex);

                context.Response.StatusCode = (int)code;
                context.Response.ContentType = "application/json";


                var payload = new ApiResponse<object>(
                    _response: new { },
                    _time: $"{sw.ElapsedMilliseconds} ms",
                    _result: false,
                    _status: (int)code,
                    _errors: msg
                    );

                await context.Response.WriteAsJsonAsync(payload);
            }
        }

        private static (HttpStatusCode code, string msg) MapException(Exception ex) => ex switch
        {
            ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
            TimeoutException => (HttpStatusCode.GatewayTimeout, ex.Message),
            HttpRequestException => (HttpStatusCode.BadGateway, ex.Message),
            _ => (HttpStatusCode.InternalServerError, ex.Message)
        };
    }
}
