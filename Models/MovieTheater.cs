using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;


namespace CineMagic.Models
{
    public class MovieTheater
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
