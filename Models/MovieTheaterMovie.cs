namespace CineMagic.Models
{
    public class MovieTheaterMovie
    {
        public int MovieTheaterId { get; set; }
        public int MovieId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public Movie Movie { get; set; }
    }
}
