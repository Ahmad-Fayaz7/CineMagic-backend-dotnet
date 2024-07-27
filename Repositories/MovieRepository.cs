using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Repositories
{
    public class MovieRepository(ApplicationDbContext dbContext) : Repository<Movie>(dbContext), IMovieRepository
    {
    }
}
