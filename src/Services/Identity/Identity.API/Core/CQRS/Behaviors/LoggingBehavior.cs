using Identity.API.Core.Interfaces;
using MediatR;
using System.Diagnostics;

namespace Identity.API.Core.CQRS.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, ILoggedRequest
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _stopwatch;

        public LoggingBehavior(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LoggingBehavior");
            _stopwatch = new Stopwatch();
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                _stopwatch.Start();
                _logger.LogInformation(
                    "{startTime}::Before execution for {requestName}",
                    DateTime.UtcNow,
                    typeof(TRequest).Name);

                return await next();
            }
            finally
            {
                _stopwatch.Stop();
                _logger.LogInformation(
                    "{startTime}::After execution for {requestName}; Request lasted:{timeTaken}ms",
                    DateTime.UtcNow,
                    typeof(TRequest).Name,
                    _stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
