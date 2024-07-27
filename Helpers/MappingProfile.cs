using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using NetTopologySuite.Geometries;

namespace CineMagic.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile(GeometryFactory geometryFactory)
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>().ReverseMap();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>().ForMember(x => x.picture, options => options.Ignore());

            CreateMap<MovieTheater, MovieTheaterDTO>()
                .ForMember(x => x.latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterCreationDTO, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.longitude, dto.latitude))));

            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MovieTheaterMovies, options => options.MapFrom(MapMovieTheaterMovies))
                .ForMember(x => x.MovieGenres, options => options.MapFrom(MapMovieGenres))
                .ForMember(x => x.MovieActors, options => options.MapFrom(MapMovieActors));




        }

        private object MapMovieActors(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieActor>();
            if (movieCreationDTO.Actors == null)
            {
                return result;
            }
            foreach (var actor in movieCreationDTO.Actors)
            {
                result.Add(new MovieActor() { ActorId = actor.Id, Character = actor.Character });
            }
            return result;
        }

        private List<MovieGenre> MapMovieGenres(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieGenre>();
            if (movieCreationDTO.GenresId == null)
            {
                return result;
            }
            foreach (var id in movieCreationDTO.GenresId)
            {
                result.Add(new MovieGenre() { GenreId = id });
            }
            return result;
        }

        private List<MovieTheaterMovie> MapMovieTheaterMovies(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieTheaterMovie>();
            if (movieCreationDTO.MovieTheatersId == null)
            {
                return result;
            }
            foreach (var id in movieCreationDTO.MovieTheatersId)
            {
                result.Add(new MovieTheaterMovie() { MovieTheaterId = id });
            }
            return result;
        }
    }
}
