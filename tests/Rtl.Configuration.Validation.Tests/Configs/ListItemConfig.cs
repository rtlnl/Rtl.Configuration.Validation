using System.ComponentModel.DataAnnotations;

namespace Rtl.Configuration.Validation.Tests.Configs
{
    class ListItemConfig
    {
        [Required]
        public string Id { get; set; }
    }
}
