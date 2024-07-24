using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Repositories
{
    public class MovieTheaterRepository(ApplicationDbContext dbContext) : Repository<MovieTheater>(dbContext), IMovieTheaterRepository
    {
        public async Task UpdateAsync(MovieTheater movieTheater)
        {
            dbContext.MovieTheaters.Update(movieTheater);
        }
    }
}
