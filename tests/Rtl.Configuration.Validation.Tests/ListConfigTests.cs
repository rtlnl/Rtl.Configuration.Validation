using Rtl.Configuration.Validation.Tests.Configs;
using Rtl.Configuration.Validation.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rtl.Configuration.Validation.Tests
{
    public class ListConfigTests
    {
        [Fact]
        public async Task ValidConfigDoesntThrow()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:0:Id"] = "1",
                ["test:1:Id"] = "1",
                ["test:2:Id"] = "1"
            };

            await TestHelpers.ValidationSucceeds<ListConfig>(settings);
        }

        [Fact]
        public void ThrowsWhenListItemValidationFails()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:0:Id"] = "1",
                ["test:1:Id"] = "",
                ["test:2:Id"] = "1"
            };

            TestHelpers.ValidationThrows<ListConfig>(settings);
        }
    }
}
