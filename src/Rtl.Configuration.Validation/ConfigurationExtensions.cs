using Microsoft.Extensions.Configuration;

namespace Rtl.Configuration.Validation
{
    public static class ConfigurationExtensions
    {
        public static T GetConfig<T>(this IConfiguration configuration, string sectionName)
            where T : class, new()
        {
            var config = configuration.GetSection(sectionName).Get<T>();

            var validator = new OptionsValidator<T>(config);
            validator.Validate();

            return config;
        }
    }
}
