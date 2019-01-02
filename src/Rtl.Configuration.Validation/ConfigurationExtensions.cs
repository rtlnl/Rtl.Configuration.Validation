using Microsoft.Extensions.Configuration;

namespace Rtl.Configuration.Validation
{
    public static class ConfigurationExtensions
    {
        public static T GetConfig<T>(this IConfiguration configuration, string sectionName)
            where T : class, new()
        {
            return configuration.GetSection(sectionName).GetConfig<T>();
        }

        public static T GetConfig<T>(this IConfiguration configuration)
            where T : class, new()
        {
            var config = configuration.Get<T>();
            var validator = new OptionsValidator<T>(config);

            validator.Validate();

            return config;
        }
    }
}
