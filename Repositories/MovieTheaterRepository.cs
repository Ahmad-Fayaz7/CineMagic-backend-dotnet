using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class MovieTheaterRepository(ApplicationDbContext dbContext) : Repository<MovieTheater>(dbContext), IMovieTheaterRepository
    {
        public async Task<List<MovieTheater>> GetNonSelectedMovieTheaters(List<int> selectedMovieTheatersIds)
        {
            var nonSelectedMovieTheaters = await dbContext.MovieTheaters.Where(x => !selectedMovieTheatersIds.Contains(x.Id)).ToListAsync();
            return nonSelectedMovieTheaters;
        }

        public async Task UpdateAsync(MovieTheater movieTheater)
        {
            dbContext.MovieTheaters.Update(movieTheater);
        }
    }
}
