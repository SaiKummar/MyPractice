using System.ComponentModel.DataAnnotations;

namespace MyPractice.API.Models
{
    public class ActorCreationDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "name cannot exceed 50")]
        public string Name { get; set; } = string.Empty;
    }
}
