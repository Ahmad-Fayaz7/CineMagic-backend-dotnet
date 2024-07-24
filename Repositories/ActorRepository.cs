using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Repositories
{
    public class ActorRepository(ApplicationDbContext dbContext) : Repository<Actor>(dbContext), IActorRepository
    {

        public async Task UpdateAsync(Actor actor)
        {
            dbContext.Actors.Update(actor);
        }
    }
}
