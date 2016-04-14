using System;
using Microsoft.Extensions.Logging;

namespace Standard
{
    internal class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(categoryName);
        }

        public void Dispose()
        {
        }

        private class CustomLogger : ILogger
        {
            private string categoryName;

            public CustomLogger(string categoryName)
            {
                this.categoryName = categoryName;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return NullScope.Instance;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(
                LogLevel logLevel, 
                EventId eventId, 
                TState state, 
                Exception exception, 
                Func<TState, Exception, string> formatter)
            {
            }

            private class NullScope : IDisposable
            {
                public static NullScope Instance = new NullScope();

                public void Dispose()
                {
                }
            }
        }
    }
}