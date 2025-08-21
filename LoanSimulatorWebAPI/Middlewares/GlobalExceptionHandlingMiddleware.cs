using FluentValidation;
using LoanSimulatorWebAPI.Dtos;
using System.Net;
using System.Text.Json;

namespace LoanSimulatorWebAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new ErrorResponse
            {
                RequestId = context.TraceIdentifier,
                StatusCode = context.Response.StatusCode,
                Message = "Validation failed",
                Errors = ex.Errors.Select(e => new ErrorDetail
                {
                    Field = e.PropertyName,
                    Message = e.ErrorMessage
                }).ToList()
            };

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }

        private static Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                RequestId = context.TraceIdentifier,
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred",
                Errors =
            [
                new() { Field = "", Message = ex.Message }
            ]
            };

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }
    }
}
