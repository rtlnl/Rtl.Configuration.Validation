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
            if (!services.Contains(new ServiceDescriptor(typeof(IStartupFilter), typeof(StartupFilter), ServiceLifetime.Transient)))
            {
                services.AddTransient<IStartupFilter, StartupFilter>();
            }

            services.Configure<T>(configuration.GetSection(sectionName));
            services.AddTransient<IOptionsValidator, OptionsValidator<T>>();

            return services;
        }

        public static IServiceCollection TryAddConfig<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where T : class, new()
        {
            if (services.Any(x => x.ServiceType == typeof(IConfigureOptions<T>)))
            {
                return services;
            }

            return services.AddConfig<T>(configuration, sectionName);
        }
    }
}
