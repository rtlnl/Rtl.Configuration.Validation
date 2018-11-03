using Rtl.Configuration.Validation.Tests.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class SuperComplexConfig
    {
        public SubConfigLevel1 SubConfigLevel1 { get; set; }
    }

    public class SubConfigLevel1
    {
        public SubConfigLevel2 SubConfigLevel2 { get; set; }
    }
    public class SubConfigLevel2
    {
        [Required]
        public string Name { get; set; }
    }
}
