using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class MovieService(IUnitOfWork unitOfWork)
    {

        public async Task AddMovieAsync(Movie movie)
        {
            await unitOfWork.Movies.AddAsync(movie);
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var movies = await unitOfWork.Movies.GetAllAsync();
            // Perform additional async operations if needed
            return movies;
        }


    }
}
