using CineMagic.DTOs;
using CineMagic.Models;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Repositories.IRepositories
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task<IQueryable<Actor>> GetActorsAsQueryable();
        Task<ActionResult<IEnumerable<MovieActorDTO>>> GetActorsByName(string name);
    }
}
