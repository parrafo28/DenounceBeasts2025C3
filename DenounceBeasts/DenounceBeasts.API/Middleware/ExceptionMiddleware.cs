using DenounceBeasts.Business.Responses;
using System.Text.Json;

namespace DenounceBeasts.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // ejecutar el siguiente middleware (o el endpoint)
            }
            catch (Exception ex)
            {
                // Atrapar cualquier excepción de abajo en el pipeline
                _logger.LogError(ex, "Unhandled exception caught by middleware");  // Logueamos el error con stacktrace

                // Preparar respuesta de error estándar
                context.Response.Clear();
                context.Response.StatusCode = ex is ArgumentException ? 400 : 500;
                context.Response.ContentType = "application/json";

                var errorMsg = context.Response.StatusCode == 400
                    ? ex.Message  // si es excepción conocida tipo ArgumentException, enviamos su mensaje
                    : "Ha ocurrido un error inesperado en el servidor"; // mensaje genérico para 500
                var isInDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
                if (isInDevelopment && context.Response.StatusCode == 500)
                {
                    errorMsg += $": {ex}"; // en desarrollo, añadimos el mensaje de la excepción

                }
                var errorResponse = new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = errorMsg,
                    Data = null,
                    StatusCode = context.Response.StatusCode
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
