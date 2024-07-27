using System.ComponentModel.DataAnnotations;

namespace CineMagic.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool? InTheaters { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Poster { get; set; }
        public List<MovieGenre>? MovieGenres { get; set; }
        public List<MovieTheaterMovie>? MovieTheaterMovies { get; set; }
        public List<MovieActor>? MovieActors { get; set; }

    }
}
