using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Rtl.Configuration.Validation.Tests.Helpers
{
    static class TestHelpers
    {
        public static void ValidationThrows<TConfig>(Dictionary<string, string> settings, string sectionName = "test")
            where TConfig : class, new()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            ValidationThrows(settings, services => services.AddConfig<TConfig>(configuration, sectionName));
        }

        public static void ValidationThrows(Dictionary<string, string> settings,
            Action<IServiceCollection> configure)
        {
            Assert.Throws<ValidationException>(() =>
            {
                var webHost = WebHost.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddInMemoryCollection(settings);
                    })
                    .ConfigureServices(services =>
                    {
                        configure(services);
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

        public static Task ValidationSucceeds<TConfig>(Dictionary<string, string> settings,
            string sectionName = "test")
            where TConfig : class, new()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            return ValidationSucceeds(settings, services => services.AddConfig<TConfig>(configuration, sectionName));
        }

        public static async Task ValidationSucceeds(Dictionary<string, string> settings,
            Action<IServiceCollection> configure)
        {
            using (var cts = new CancellationTokenSource())
            {
                var webHost = WebHost.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddInMemoryCollection(settings);
                    })
                    .ConfigureServices(services =>
                    {
                        configure(services);
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