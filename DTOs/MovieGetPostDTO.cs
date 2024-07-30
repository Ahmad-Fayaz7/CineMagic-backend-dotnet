namespace CineMagic.DTOs
{
    public class MovieGetPostDTO
    {
        public List<GenreDTO> Genres { get; set; }
        public List<MovieTheaterDTO> MovieTheaters { get; set; }
    }
}
