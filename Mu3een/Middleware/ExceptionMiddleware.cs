using Mu3een.Errors;
using System.Net;
using System.Text.Json;

namespace Mu3een.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger,IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception error)
            {
                _logger.LogError(error, error.Message);
                context.Response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

               
                var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode, error.Message, error.StackTrace??"".ToString()) :
                    new ApiException(context.Response.StatusCode, error.Message);
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

    }

}
