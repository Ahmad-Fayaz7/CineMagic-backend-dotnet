using CineMagic.Models;

namespace CineMagic.Repositories.IRepositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<List<Genre>> GetNonSelectedGenres(List<int> selectedGenresIds);
        Task UpdateAsync(Genre genre);
    }
}
