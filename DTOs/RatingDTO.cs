using System.ComponentModel.DataAnnotations;

namespace CineMagic.DTOs
{
    public class RatingDTO
    {
        [Range(1, 5)]
        public int Rate { get; set; }
        public int MovieId { get; set; }
    }
}
