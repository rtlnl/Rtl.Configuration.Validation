using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rtl.Configuration.Validation.Tests.Configs
{
    class ListConfig : List<ListItemConfig>
    {
    }

    class ListItemConfig
    {
        [Required]
        public string Id { get; set; }
    }
}
