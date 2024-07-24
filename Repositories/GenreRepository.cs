using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task UpdateAsync(Genre genre)
        {
            _dbContext.Genres.Update(genre);
        }

    }
}
