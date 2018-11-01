using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rtl.Configuration.Validation
{
    public static class ConfigurationExtentions
    {
        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where T : class
        {
            var config = configuration.GetSection(sectionName).Get<T>();
            Validator.ValidateObject(config, new ValidationContext(config), validateAllProperties: true);
            services.AddSingleton(config);
            return services;
        }
    }
}
