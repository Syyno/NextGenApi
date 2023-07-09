using MediatR;
using Microsoft.Extensions.Logging;

namespace Payments.Application.Common.Behavior;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling command '{Name}' begin with payload: {@Payload}", typeof(TRequest).UnderlyingSystemType, request);

        var response = await next();
        
        _logger.LogInformation("Command '{Name}' handled with response: {@Response}", typeof(TRequest).UnderlyingSystemType, response);

        return response;
    }
}
