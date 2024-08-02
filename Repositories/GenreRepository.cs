using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Genre>> GetNonSelectedGenres(List<int> selectedGenresIds)
        {
            return await _dbContext.Genres.Where(x => !selectedGenresIds.Contains(x.Id)).ToListAsync();

        }

        public async Task UpdateAsync(Genre genre)
        {
            _dbContext.Genres.Update(genre);
        }

    }
}
