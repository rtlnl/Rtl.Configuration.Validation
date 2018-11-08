using System.ComponentModel.DataAnnotations;

namespace Rtl.Configuration.Validation.Tests.Configs
{
    public class TestConfiguration
    {
        [Required]
        public string Name { get; set; }

        [Range(0, 10)]
        public int Value { get; set; }
    }
}
