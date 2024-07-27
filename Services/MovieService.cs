using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class MovieService(IMovieRepository movieRepository)
    {

        public async Task AddMovieAsync(Movie movie)
        {
            await movieRepository.AddAsync(movie);
        }

        public async Task SaveMovieChangesAsync()
        {
            await movieRepository.SaveChangesAsync();
        }
    }
}
