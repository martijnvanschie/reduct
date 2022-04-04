using Xunit;
using FluentAssertions;
using Reduct.System;
using System;
using Xunit.Abstractions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Reduct.Framework.Tests.Azure.EventGrid
{
    public class EventGridPublisherTests
    {
        private readonly ITestOutputHelper output;
        IConfiguration configuration;

        public EventGridPublisherTests(ITestOutputHelper output)
        {
            this.output = output;
            configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }
    }
}