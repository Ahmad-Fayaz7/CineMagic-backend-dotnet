using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IQueryable<Movie>> GetAllMoviesAsQueryableAsync();
        Task<List<Movie>> GetMoviesInTheater();
        Task<Movie> GetMovieWithDetails(int id);
        Task<List<Movie>> GetUpcomingMovies();
    }
}
