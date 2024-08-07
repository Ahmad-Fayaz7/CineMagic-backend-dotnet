using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class RatingRepository(ApplicationDbContext context) : Repository<Rating>(context), IRatingRepository
    {
        public async Task<double> GetAverageRateAsync(int movieId)
        {

            var rating = await context.Ratings.AnyAsync(x => x.MovieId == movieId);
            if (rating)
            {
                return await context.Ratings.Where(x => x.MovieId == movieId).AverageAsync(x => x.Rate);
            }
            return 0.0;
        }

        public async Task<Rating> GetCurrentRate(string userId, int movieId)
        {
            return await context.Ratings.FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId);
        }


    }
}
