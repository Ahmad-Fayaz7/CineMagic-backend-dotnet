using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task UpdateAsync(Actor actor);
    }
}
