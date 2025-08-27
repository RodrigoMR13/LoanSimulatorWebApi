using Application.Exceptions;
using FluentValidation;
using LoanSimulatorWebAPI.Dtos;
using System.Net;
using System.Text.Json;

namespace LoanSimulatorWebAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred.");
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (ProductNotFoundException ex)
            {
                _logger.LogError(ex, "Product not found.");
                await HandleProductNotFoundExceptionAsync(context, ex);
            }
            catch (NoSuitableProductFoundException ex)
            {
                _logger.LogError(ex, "No Suitable Product Found.");
                await HandleNoSuitableProductFoundExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
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

        private static Task HandleProductNotFoundExceptionAsync(HttpContext context, ProductNotFoundException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var response = new ErrorResponse
            {
                RequestId = context.TraceIdentifier,
                StatusCode = context.Response.StatusCode,
                Message = "Not Found",
                Errors =
                [
                    new() { Field = "", Message = ex.Message }
                ]
            };

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }

        private static Task HandleNoSuitableProductFoundExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

            var response = new ErrorResponse
            {
                RequestId = context.TraceIdentifier,
                StatusCode = context.Response.StatusCode,
                Message = "No Suitable Product Found",
                Errors =
                [
                    new() { Field = "", Message = ex.Message }
                ]
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
