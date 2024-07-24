using System.ComponentModel.DataAnnotations;

namespace CineMagic.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? picture { get; set; }

        public string? Biography { get; set; }
    }
}
