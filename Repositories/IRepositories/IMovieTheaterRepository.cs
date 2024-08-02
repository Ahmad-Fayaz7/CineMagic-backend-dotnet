using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IMovieTheaterRepository : IRepository<MovieTheater>
    {
        Task<List<MovieTheater>> GetNonSelectedMovieTheaters(List<int> selectedMovieTheatersIds);
        Task UpdateAsync(MovieTheater movieTheater);
    }
}
