using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class MovieTheaterService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<IEnumerable<MovieTheater>> GetAllMovieTheatersAysnc()
        {
            return await unitOfWork.MoviesTheaters.GetAllAsync();

        }

        public async Task<MovieTheater> GetMovieTheaterByIdAsync(int id)
        {
            return await unitOfWork.MoviesTheaters.GetByIdAsync(id);
        }

        public async Task AddMovieTheaterAsync(MovieTheater movieTheater)
        {
            await unitOfWork.MoviesTheaters.AddAsync(movieTheater);
        }

        public async Task<bool> UpdateMovieTheaterAsync(int id, MovieTheaterDTO movieTheaterDTO)
        {
            if (!await unitOfWork.MoviesTheaters.ExistsAsync(id))
            {
                return false;
            }

            var movieTheater = await unitOfWork.MoviesTheaters.GetByIdAsync(id);
            mapper.Map(movieTheaterDTO, movieTheater);
            await unitOfWork.MoviesTheaters.UpdateAsync(movieTheater);

            return true;
        }

        public async Task<bool> DeleteMovieTheaterAsync(int id)
        {
            if (!await unitOfWork.MoviesTheaters.ExistsAsync(id))
            {
                return false;
            }
            var movieTheater = await unitOfWork.MoviesTheaters.GetByIdAsync(id);
            await unitOfWork.MoviesTheaters.RemoveAsync(movieTheater);

            return true;
        }

    }
}
