using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        [StringLength(100)]
        [DefaultValue("PlatformName")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [DefaultValue("Publisher")] 
        public string Publisher { get; set; }

        [Required]
        [StringLength(30)]
        [DefaultValue("Free")]
        public string Cost { get; set; }

        [DefaultValue(5)]
        public int test { get; set; }
    }
}
