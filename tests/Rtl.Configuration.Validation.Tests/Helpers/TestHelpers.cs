using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rtl.Configuration.Validation.Tests.Helpers
{
    static class TestHelpers
    {
        public static void ValidationThrows<TConfig>(Dictionary<string, string> settings, string sectionName = "test")
            where TConfig : class, new()
        {
            IConfiguration configuration = null;

            Assert.Throws<ValidationException>(() =>
            {
                var webHost = WebHost.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddInMemoryCollection(settings);
                        configuration = config.Build();
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddConfig<TConfig>(configuration, sectionName);
                    })
                    .Configure(app =>
                    {
                        throw new Exception("Should not come to this point");
                    })
                    .Build();

                using (webHost)
                    webHost.Run();
            });
        }

        public static async Task ValidationSucceeds<TConfig>(Dictionary<string, string> settings, string sectionName = "test")
            where TConfig : class, new()
        {
            IConfiguration configuration = null;
            using (var cts = new CancellationTokenSource())
            {
                var webHost = WebHost.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddInMemoryCollection(settings);
                        configuration = config.Build();
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddConfig<TConfig>(configuration, sectionName);
                    })
                    .Configure(app =>
                    {
                        cts.Cancel();
                    })
                    .Build();

                using (webHost)
                    await webHost.RunAsync(cts.Token);
            }
        }
    }
}
