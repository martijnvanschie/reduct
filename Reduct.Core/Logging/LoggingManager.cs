using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Reduct.Core.Logging
{
    /// <summary>
    /// https://code-maze.com/dotnet-ilogger-iloggerfactory-iloggerprovider/
    /// </summary>
    public class LoggingManager
    {
        public static ILoggerFactory Factory { get; }

        static LoggingManager()
        {
            Factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
        }

        public static ILogger<T> GetNullLogger<T>()
        {
            return NullLogger<T>.Instance;
        }
    }
}