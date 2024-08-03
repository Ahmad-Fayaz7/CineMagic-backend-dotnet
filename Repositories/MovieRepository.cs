using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Repositories
{
    public class MovieRepository(ApplicationDbContext dbContext) : Repository<Movie>(dbContext), IMovieRepository
    {
        public async Task<IQueryable<Movie>> GetAllMoviesAsQueryableAsync()
        {
            return dbContext.Movie.AsQueryable();
        }

        public async Task<List<Movie>> GetMoviesInTheater()
        {
            var top = 6;
            var moviesInTheater = await dbContext.Movie.Where(x => (bool)x.InTheaters).OrderBy(x => x.ReleaseDate).Take(top).ToListAsync();
            return moviesInTheater;
        }

        public async Task<Movie> GetMovieWithDetails(int id)
        {
            var movie = dbContext.Movie
                .Include(x => x.MovieGenres).ThenInclude(x => x.Genre)
                .Include(x => x.MovieTheaterMovies).ThenInclude(x => x.MovieTheater)
                .Include(x => x.MovieActors).ThenInclude(x => x.Actor)
                .FirstOrDefault(x => x.Id == id);

            return movie;
        }

        public async Task<List<Movie>> GetUpcomingMovies()
        {
            var top = 6;
            var today = DateTime.Today;
            var upcomingMovies = await dbContext.Movie.Where(x => x.ReleaseDate > today).OrderBy(x => x.ReleaseDate).ToListAsync();
            return upcomingMovies;
        }
    }
}
