﻿namespace CineMagic.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool? InTheaters { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Poster { get; set; }
        public double AverageRate { get; set; }
        public int UserRate { get; set; }
        public List<GenreDTO>? Genres { get; set; }
        public List<MovieTheaterDTO>? MovieTheaters { get; set; }
        public List<MovieActorDTO>? Actors { get; set; }

    }
}
