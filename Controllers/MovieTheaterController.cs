using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Controllers
{
    [Route("api/movietheaters")]
    [ApiController]
    public class MovieTheaterController(MovieTheaterService movieTheaterService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> GetMovieTheaters()
        {
            var movieTheaters = await movieTheaterService.GetAllMovieTheatersAysnc();
            return mapper.Map<List<MovieTheaterDTO>>(movieTheaters);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieTheaterDTO>> GetMovieTheaterById(int id)
        {
            var movie = await movieTheaterService.GetMovieTheaterByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return mapper.Map<MovieTheaterDTO>(movie);

        }

        [HttpPost]
        public async Task<ActionResult> AddMovieTheater(MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheater = mapper.Map<MovieTheater>(movieTheaterCreationDTO);
            await movieTheaterService.AddMovieTheaterAsync(movieTheater);
            await movieTheaterService.SaveMovieTheaterChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditMovieTheater(int id, MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheater = await movieTheaterService.GetMovieTheaterByIdAsync(id);
            if (movieTheater == null) { return NotFound(); }
            movieTheater = mapper.Map(movieTheaterCreationDTO, movieTheater);
            await movieTheaterService.SaveMovieTheaterChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMovieTheater(int id)
        {
            var movieTheater = await movieTheaterService.GetMovieTheaterByIdAsync(id);
            if (movieTheater == null)
            {
                return NotFound();
            }
            await movieTheaterService.DeleteMovieTheaterAsync(id);
            await movieTheaterService.SaveMovieTheaterChangesAsync();
            return NoContent();
        }

    }
}