using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Rtl.Configuration.Validation
{
    class OptionsValidator<T> : IOptionsValidator where T : class, new()
    {
        private readonly T _options;

        public OptionsValidator(IOptions<T> options)
        {
            _options = options.Value;
        }

        public void Validate()
        {
            Validator.ValidateObject(_options, new ValidationContext(_options), validateAllProperties: true);
        }
    }
}
