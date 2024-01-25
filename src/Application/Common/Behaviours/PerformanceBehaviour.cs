using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Sample2.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(
        ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("CleanArchitecture Processing Time: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsedMilliseconds, request);

        if (elapsedMilliseconds > 500)
        {
            _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds)", requestName, elapsedMilliseconds);
        }

        return response;
    }
}
