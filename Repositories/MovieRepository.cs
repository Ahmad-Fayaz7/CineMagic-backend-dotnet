using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class MovieRepository(ApplicationDbContext dbContext) : Repository<Movie>(dbContext), IMovieRepository
    {
        public async Task<Movie> GetMovieWithDetails(int id)
        {
            var movie = dbContext.Movie
                .Include(x => x.MovieGenres).ThenInclude(x => x.Genre)
                .Include(x => x.MovieTheaterMovies).ThenInclude(x => x.MovieTheater)
                .Include(x => x.MovieActors).ThenInclude(x => x.Actor)
                .FirstOrDefault(x => x.Id == id);

            return movie;
        }
    }
}
