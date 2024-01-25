using Microsoft.AspNetCore.Mvc;
using Sample2.Application.Common.Exceptions;

namespace Sample2.API.Infrastructure;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;

        // Register known exception types and handlers.
        _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ConflictException), HandleConflictException },
            };
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        Type exceptionType = exception.GetType();
        _logger.LogError(default, exception, "CleanArchitecture Exception Middleware: Processing error type {ErrorType}", exceptionType);

        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
        }
        else
        {
            await HandleUnhandledException(httpContext);
        }
    }

    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        ValidationException exception = (ValidationException)ex;

        httpContext.Response.StatusCode = (int)exception.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = exception.Message,
            Status = (int)exception.StatusCode,
            Detail = exception.ErrorDetailMessage,
            Instance = httpContext.Request.Path.Value
        });
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        NotFoundException exception = (NotFoundException)ex;

        httpContext.Response.StatusCode = (int)exception.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "The specified resource was not found.",
            Status = (int)exception.StatusCode,
            Detail = exception.ErrorDetailMessage,
            Instance = httpContext.Request.Path.Value
        });
    }

    private async Task HandleConflictException(HttpContext httpContext, Exception ex)
    {
        ConflictException exception = (ConflictException)ex;

        httpContext.Response.StatusCode = (int)exception.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "The specified resource was conflicted.",
            Status = (int)exception.StatusCode,
            Detail = exception.ErrorDetailMessage,
            Instance = httpContext.Request.Path.Value
        });
    }

    private static async Task HandleUnhandledException(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "Internal server error occurred.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unhandle error occurred while processing request.",
            Instance = httpContext.Request.Path.Value
        });
    }
}