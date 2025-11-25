using DenounceBeasts.Application.Responses;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace DenounceBeasts.Presentation.Middleware
{
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Guardar el stream original
            var originalBodyStream = context.Response.Body;
            await using var memStream = new MemoryStream();
            context.Response.Body = memStream;
            try
            {
                await _next(context); // ejecutar la acción del controlador
            }
            finally
            {
                // Resetear el stream al original para poder escribir la respuesta final
                context.Response.Body = originalBodyStream;
            }

            // Si la respuesta ya tiene un código StatusCode indicado por algo anterior, lo tomamos
            int statusCode = context.Response.StatusCode;
            // Volcar el contenido generado en memoria a un string
            memStream.Seek(0, SeekOrigin.Begin);
            string bodyText = await new StreamReader(memStream, Encoding.UTF8).ReadToEndAsync();

            // Caso: No hay contenido generado (por ejemplo, 204 NoContent)
            if (string.IsNullOrWhiteSpace(bodyText) && (statusCode == StatusCodes.Status204NoContent || statusCode == 0))
            {
                // En respuestas vacías, opcionalmente podríamos generar un cuerpo con success = true y mensaje.
                // Pero 204 por estándar no lleva cuerpo. Así que no envolvemos nada.
                context.Response.StatusCode = statusCode == 0 ? 204 : statusCode;
                return;
            }

            // Determinar si ya es un ApiResponse
            bool alreadyWrapped = false;
            try
            {
                // Una manera simple: verificar si JSON tiene la propiedad "success"
                using JsonDocument doc = JsonDocument.Parse(bodyText);
                if (doc.RootElement.TryGetProperty("success", out _))
                {
                    alreadyWrapped = true;
                }
            }
            catch
            { /* si parsea falla, no es JSON válido, lo trataremos abajo */

            }

            object? newBodyObject = null;
            string newBodyJson;

            if (!alreadyWrapped)
            {
                if (statusCode >= 400)
                {
                    // Si fue error y no estaba envuelto, crear ApiResponse de error
                    string errorMessage;
                    if (!string.IsNullOrEmpty(bodyText))
                    {
                        // Si hay un body (ej: ProblemDetails de modelo), podríamos extraer mensaje.
                        errorMessage = bodyText;
                    }
                    else
                    {
                        // Mensaje genérico basado en código
                        errorMessage = statusCode == 404 ? "Recurso no encontrado" :
                                       statusCode == 400 ? "Solicitud inválida" :
                                       "Error";
                    }
                    newBodyObject = new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Message = errorMessage,
                        Data = null,
                        StatusCode = statusCode
                    };
                }
                else
                {
                    // Respuesta exitosa no envuelta: envolver data
                    if (string.IsNullOrEmpty(bodyText))
                    {
                        // Por si acaso, si es éxito sin body (poco común salvo endpoints void), enviamos success true.
                        newBodyObject = new ApiResponse<string>
                        {
                            IsSuccess = true,
                            Message = "Operación realizada correctamente",
                            Data = null,
                            StatusCode = statusCode
                        };
                    }
                    else
                    {
                        // Intentar deserializar el body existente a un objeto .NET
                        try
                        {
                            newBodyObject = JsonSerializer.Deserialize<object>(bodyText);
                        }
                        catch
                        {
                            // Si el body no es JSON (ej, es un string plano), lo usamos tal cual
                            newBodyObject = bodyText;
                        }
                        newBodyObject = ApiResponse<object>.Success(newBodyObject, statusCode);
                    }
                }

                // Serializar el nuevo cuerpo envuelto a JSON
                newBodyJson = JsonSerializer.Serialize(newBodyObject);
            }
            else
            {
                // Si ya estaba envuelto correctamente, usamos el body tal cual
                newBodyJson = bodyText;
            }

            // Escribir el nuevo JSON en el response
            context.Response.ContentType = "application/json";
            // Asegurarse de setear el statusCode apropiado (pudo haber sido 0 si no se seteó antes)
            context.Response.StatusCode = statusCode == 0 ? 200 : statusCode;
            await context.Response.WriteAsync(newBodyJson, Encoding.UTF8);
        }
    }

}