using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Rtl.Configuration.Validation
{
    class StartupFilter : IStartupFilter
    {
        private readonly IEnumerable<IOptionsValidator> _optionsValidators;

        public StartupFilter(IEnumerable<IOptionsValidator> optionsValidators)
        {
            _optionsValidators = optionsValidators;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var optionsValidator in _optionsValidators)
            {
                optionsValidator.Validate();
            }

            return next;
        }
    }
}
