using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class RatingRepository(ApplicationDbContext context) : Repository<Rating>(context), IRatingRepository
    {
        public async Task<Rating> GetCurrentRate(string userId, int movieId)
        {
            return await context.Ratings.FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId);
        }
    }
}
