using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class ActorRepository(ApplicationDbContext dbContext) : Repository<Actor>(dbContext), IActorRepository
    {

        public async Task UpdateAsync(Actor actor)
        {
            dbContext.Actors.Update(actor);
        }

        public async Task<IQueryable<Actor>> GetActorsAsQueryable()
        {
            return dbContext.Actors.AsQueryable();
        }

        public async Task<ActionResult<IEnumerable<MovieActorDTO>>> GetActorsByName(string name)
        {
            return await dbContext.Actors.Where(autor => autor.Name.Contains(name)).OrderBy(autor => autor.Name).Select(x => new MovieActorDTO { Id = x.Id, Name = x.Name, Picture = x.picture }).Take(5).ToListAsync();

        }
    }
}
