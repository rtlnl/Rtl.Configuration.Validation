using System.ComponentModel.DataAnnotations;

namespace Rtl.Configuration.Validation.Tests.Configs
{
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
