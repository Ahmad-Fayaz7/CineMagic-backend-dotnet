using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using Microsoft.AspNet.Identity.EntityFramework;
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


            CreateMap<Movie, MovieDTO>()
                .ForMember(x => x.Genres, options => options.MapFrom(MapMovieGenres))
                .ForMember(x => x.Actors, options => options.MapFrom(MapMovieActors))
                .ForMember(x => x.MovieTheaters, options => options.MapFrom(MapMovieTheaters));

            CreateMap<IdentityUser, UserDTO>();



        }


        private List<MovieTheaterDTO> MapMovieTheaters(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<MovieTheaterDTO>();
            if (movie.MovieTheaterMovies != null)
            {
                foreach (var movieTheater in movie.MovieTheaterMovies)
                {
                    result.Add(new MovieTheaterDTO { Id = movieTheater.MovieTheaterId, Name = movieTheater.MovieTheater.Name, latitude = movieTheater.MovieTheater.Location.Y, longitude = movieTheater.MovieTheater.Location.X });
                }
            }

            return result;
        }


        private List<MovieActorDTO> MapMovieActors(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<MovieActorDTO>();
            if (movie.MovieActors != null)
            {
                foreach (var actor in movie.MovieActors)
                {
                    result.Add(new MovieActorDTO { Id = actor.ActorId, Name = actor.Actor.Name, Character = actor.Character, Picture = actor.Actor.picture, Order = actor.Order });
                }
            }

            return result;
        }

        private List<GenreDTO> MapMovieGenres(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<GenreDTO>();
            if (movie.MovieGenres != null)
            {
                foreach (var genre in movie.MovieGenres)
                {
                    result.Add(new GenreDTO { Id = genre.GenreId, Name = genre.Genre.Name });
                }
            }

            return result;
        }

        private List<MovieActor> MapMovieActors(MovieCreationDTO movieCreationDTO, Movie movie)
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
            if (movieCreationDTO.GenresIds == null)
            {
                return result;
            }
            foreach (var id in movieCreationDTO.GenresIds)
            {
                result.Add(new MovieGenre() { GenreId = id });
            }
            return result;
        }

        private List<MovieTheaterMovie> MapMovieTheaterMovies(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieTheaterMovie>();
            if (movieCreationDTO.MovieTheatersIds == null)
            {
                return result;
            }
            foreach (var id in movieCreationDTO.MovieTheatersIds)
            {
                result.Add(new MovieTheaterMovie() { MovieTheaterId = id });
            }
            return result;
        }
    }
}
