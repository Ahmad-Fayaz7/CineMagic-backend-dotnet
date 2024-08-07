using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class MovieService(IUnitOfWork unitOfWork)
    {

        public async Task AddMovieAsync(Movie movie)
        {
            await unitOfWork.Movies.AddAsync(movie);
            await unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var movies = await unitOfWork.Movies.GetAllAsync();
            // Perform additional async operations if needed
            return movies;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await unitOfWork.Movies.GetByIdAsync(id);
            return movie;
        }

        public async Task<Movie> GetMovieWithDetails(int id)
        {
            var movie = await unitOfWork.Movies.GetMovieWithDetails(id);
            return movie;
        }

        public async Task<List<Movie>> GetMovieshInTheaters()
        {
            var moviesInTheater = await unitOfWork.Movies.GetMoviesInTheater();
            return moviesInTheater;
        }

        public async Task<List<Movie>> GetUpcomingMovies()
        {
            var upcomingMovies = await unitOfWork.Movies.GetUpcomingMovies();
            return upcomingMovies;
        }

        public Task<IQueryable<Movie>> GetAllMoviesAsQueryableAsync()
        {
            return unitOfWork.Movies.GetAllMoviesAsQueryableAsync();
        }

        public async Task DeleteMovie(Movie movie)
        {
            await unitOfWork.Movies.RemoveAsync(movie);
        }

        public async Task<Rating> GetCurrentRate(string userId, int movieId)
        {
            return await unitOfWork.Rates.GetCurrentRate(userId, movieId);
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await unitOfWork.Rates.AddAsync(rating);
        }

        public async Task<double> GetRatingAverageAsync(int movieId)
        {
            var averageRating = await unitOfWork.Rates.GetAverageRateAsync(movieId);
            return averageRating;
        }


    }
}
