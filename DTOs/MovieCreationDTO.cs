using CineMagic.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.DTOs
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool? InTheaters { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IFormFile? Poster { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresId { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> MovieTheatersId { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<MovieActorCreationDTO>>))]
        public List<MovieActorCreationDTO> Actors { get; set; }
    }
}
