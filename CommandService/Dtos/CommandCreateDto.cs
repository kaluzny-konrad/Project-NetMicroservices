using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        public string HowTo { get; set; } = null!;

        [Required]
        public string CommandLine { get; set; } = null!;
    }
}
