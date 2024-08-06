using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<Rating> GetCurrentRate(string userId, int movieId);
    }
}
