using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetMovieWithDetails(int id);
    }
}
