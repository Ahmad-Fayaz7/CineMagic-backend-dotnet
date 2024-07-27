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
    public class MovieController(MovieService movieService, IMapper mapper, InAppStorageService inAppStorageService) : ControllerBase
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
            await movieService.SaveMovieChangesAsync();
            return NoContent();
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
