using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Reduct.Framework.Tests.Fixtures
{
    public class DefaultFixture : IDisposable
    {
        internal ILoggerFactory LoggerFactoryInstance;
        internal IConfiguration Configuration;

        public DefaultFixture()
        {
            Configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            var _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            LoggerFactoryInstance = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(_config.GetSection("Logging"));
            });
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
