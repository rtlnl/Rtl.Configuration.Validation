using Rtl.Configuration.Validation.Tests.Configs;
using Rtl.Configuration.Validation.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Rtl.Configuration.Validation.Tests
{
    public class SimpleConfigsTests
    {
        [Fact]
        public async Task ValidConfigDoesntThrow()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Name"] = "asd",
                ["test:Age"] = "12"
            };

            await TestHelpers.ValidationSucceeds<TestConfiguration>(settings);
        }

        [Fact]
        public void NoConfigThrows()
        {
            var settings = new Dictionary<string, string>();

            TestHelpers.ValidationThrows<TestConfiguration>(settings);
        }

        [Fact]
        public void InvalidConfigThrows()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Name"] = ""
            };

            TestHelpers.ValidationThrows<TestConfiguration>(settings);
        }

        [Fact]
        public void PartiallyInvalidConfigThrows()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Name"] = "",
                ["test:Value"] = "3"
            };

            TestHelpers.ValidationThrows<TestConfiguration>(settings);
        }
    }
}
