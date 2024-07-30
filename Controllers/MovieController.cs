using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Helpers;
using CineMagic.Models;
using CineMagic.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController(MovieService movieService, IMapper mapper, IFileStorageService inAppStorageService, GenreService genreService, MovieTheaterService movieTheaterService, ActorService actorService) : ControllerBase
    {
        private readonly string containerName = "movies";

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movie = mapper.Map<Movie>(movieCreationDTO);
            if (movieCreationDTO.Poster != null)
            {
                movie.Poster = await inAppStorageService.SaveFile(containerName, movieCreationDTO.Poster);
            }
            AddOrderToMovie(movie);
            await movieService.AddMovieAsync(movie);

            return NoContent();
        }
        [HttpGet("GetPost")]
        public async Task<ActionResult<MovieGetPostDTO>> GetPost()
        {
            var genres = await genreService.GetAllAsync();
            genres = genres.OrderBy(x => x.Name);
            var movieTheaters = await movieTheaterService.GetAllMovieTheatersAysnc();
            movieTheaters = movieTheaters.OrderBy(x => x.Name);
            var genresDto = mapper.Map<List<GenreDTO>>(genres);
            var movieTheatersDto = mapper.Map<List<MovieTheaterDTO>>(movieTheaters);
            var movieGetPostDto = new MovieGetPostDTO { Genres = genresDto, MovieTheaters = movieTheatersDto };

            return movieGetPostDto;
        }



        private void AddOrderToMovie(Movie movie)
        {
            if (movie.MovieActors != null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i;
                }
            }

        }
    }
}
