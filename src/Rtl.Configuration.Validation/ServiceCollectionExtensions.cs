using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Rtl.Configuration.Validation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where T : class, new()
        {
            return services.AddConfig<T>(configuration.GetSection(sectionName));
        }

        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, new()
        {
            if (services.Any(x => x.ServiceType == typeof(IConfigureOptions<T>)))
            {
                return services;
            }

            if (!services.Any(x => x.ImplementationType == typeof(StartupFilter)))
            {
                services.AddTransient<IStartupFilter, StartupFilter>();
            }

            services.Configure<T>(configuration);
            services.AddTransient<IOptionsValidator, OptionsValidator<T>>();

            return services;
        }
    }
}