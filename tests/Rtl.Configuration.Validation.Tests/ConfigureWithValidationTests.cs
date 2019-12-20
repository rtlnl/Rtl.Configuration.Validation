using System;
using System.Threading.Tasks;
using Rtl.Configuration.Validation.Tests.Configs;
using Rtl.Configuration.Validation.Tests.Helpers;
using Xunit;

namespace Rtl.Configuration.Validation.Tests
{
    public class ConfigureTests
    {
        [Fact]
        public async Task ConfigureWithValidationDoesntThrow()
        {
            await TestHelpers.ValidationSucceeds(null, services =>
                services.ConfigureWithValidation<TestConfiguration>(option =>
                {
                    option.Name = "asd";
                    option.Value = 5;
                }));
        }

        [Fact]
        public void ConfigureWithValidationThrows()
        {
            TestHelpers.ValidationThrows(null, services =>
                services.ConfigureWithValidation<TestConfiguration>(option =>
                    option.Name = string.Empty));
        }
    }
}