using System.ComponentModel.DataAnnotations;

namespace MyPractice.API.Models
{
    public class ActorUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
