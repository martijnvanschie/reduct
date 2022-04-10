using Microsoft.Extensions.Configuration;
using System;

namespace Reduct.Framework.Tests.Fixtures
{
    public class DefaultFixture : IDisposable
    {
        internal IConfiguration Configuration;

        public DefaultFixture()
        {
            Configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
