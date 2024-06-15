using Identity.API.Core.Queries;
using MediatR;

namespace Identity.API.Core.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: notnull, ILoggedRequest
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LoggingBehavior");
        }
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($@"Before execution for {typeof(TRequest).Name};
Started at: {DateTime.UtcNow}");

                return await next();
            }
            finally
            {
                _logger.LogInformation(@$"After execution for {typeof(TRequest).Name}
Completed at: {DateTime.UtcNow}");
            }
        }
    }
}
