using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Helpers;
using CineMagic.Models;
using CineMagic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorController(ActorService actorService, IMapper mapper, IFileStorageService fileStorageService) : ControllerBase
    {
        private readonly string containerName = "actors";

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = await actorService.GetActorsAsQueryable();

            await HttpContext.InsertPaginationParametersInHeader<Actor>(queryable);
            var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<ActorDTO>>(actors);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await actorService.GetActorByIdAsync(id);

            return mapper.Map<ActorDTO>(actor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
        {
            var actor = mapper.Map<Actor>(actorCreationDTO);
            if (actorCreationDTO.picture != null)
            {
                actor.picture = await fileStorageService.SaveFile(containerName, actorCreationDTO.picture);
            }
            await actorService.AddActorAsync(actor);

            return NoContent();
        }

        [HttpPost("SearchByName")]
        public async Task<ActionResult<IEnumerable<MovieActorDTO>>> SearchByName([FromBody] string name)
        {
            return await actorService.GetActorsByName(name);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
        {
            var actor = await actorService.GetActorByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            actor = mapper.Map(actorCreationDTO, actor);
            if (actorCreationDTO.picture != null)
            {
                actor.picture = await fileStorageService.EditFile(containerName, actorCreationDTO.picture, actor.picture);
            }

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await actorService.GetActorByIdAsync(id);
            if (actor == null) { return NotFound(); }
            await actorService.DeleteActorAsync(actor.Id);

            await fileStorageService.DeleteFile(actor.picture, containerName);
            return NoContent();
        }
    }
}
