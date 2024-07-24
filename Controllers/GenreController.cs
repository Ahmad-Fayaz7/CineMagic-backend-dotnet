
using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController(GenreService genresService, IMapper mapper) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {

            var genres = await genresService.GetAllAsync();
            genres = genres.OrderBy(g => g.Name);
            return mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> Get(int Id)
        {
            var genre = await genresService.GetByIdAsync(Id);
            return mapper.Map<GenreDTO>(genre);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDto)
        {
            var genre = mapper.Map<Genre>(genreCreationDto);
            await genresService.AddAsync(genre);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDto)
        {
            var updated = await genresService.UpdateGenreAsync(id, genreCreationDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await genresService.DeleteGenreAsync(id);
            return NoContent();
        }


    }
}
