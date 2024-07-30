using CineMagic.Models;


namespace CineMagic.Repositories.IRepositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task UpdateAsync(Genre genre);
    }
}
