using Rtl.Configuration.Validation.Tests.Configs;
using Rtl.Configuration.Validation.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rtl.Configuration.Validation.Tests
{
    public class SuperComplexConfigTests
    {
        [Fact]
        public async Task ValidConfigDoesntThrow()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:SubConfigLevel1:SubConfigLevel2:Name"] = "name",
            };

            await TestHelpers.ValidationSucceeds<SuperComplexConfig>(settings);
        }

        [Fact]
        public void ThrowsWhenNestedConfigIsInvalid()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:SubConfigLevel1:SubConfigLevel2:Name"] = "",
            };

            TestHelpers.ValidationThrows<SuperComplexConfig>(settings);
        }
    }
}
