using System.ComponentModel.DataAnnotations;

namespace CineMagic.DTOs
{
    public class MovieTheaterCreationDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(-90, 90)]
        public double latitude { get; set; }
        [Range(-180, 180)]
        public double longitude { get; set; }
    }
}
