using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IMovieTheaterRepository : IRepository<MovieTheater>
    {
        Task UpdateAsync(MovieTheater movieTheater);
    }
}
