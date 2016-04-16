using Microsoft.Extensions.Logging;
using System;

namespace LoggingSample
{
    public interface IExplodingEvent
    {
        void Log(long elapsedMilliseconds);
    }

    public class ExplodingEvent : IExplodingEvent
    {
        private static readonly Action<ILogger, long ,Exception> LogAction = LoggerMessage.Define<long>(
            LogLevel.Warning, 
            new EventId(0, "Exploding"), 
            "Exploding {elapsed}ms since start");

        private readonly ILogger logger;
        
        public ExplodingEvent(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentException(nameof(loggerFactory));
            }
            this.logger = loggerFactory.CreateLogger("LoggingSample.ExplodingEvent");
        }

        public void Log(long elapsedMilliseconds)
        {
            LogAction(logger, elapsedMilliseconds, null);
        }
    }
}
