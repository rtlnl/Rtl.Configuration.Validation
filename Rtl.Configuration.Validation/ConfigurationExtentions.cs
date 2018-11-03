using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rtl.Configuration.Validation
{
    public static class ConfigurationExtentions
    {
        public static bool _startupFilterAdded = false;

        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where T : class, new()
        {
            if (!_startupFilterAdded)
            {
                services.AddTransient<IStartupFilter, StartupFilter>();
                _startupFilterAdded = true;
            }
            
            services.Configure<T>(configuration.GetSection(sectionName));
            services.AddTransient<IOptionsValidator, OptionsValidator<T>>();

            return services;
        }
    }
}
