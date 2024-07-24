using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class MovieTheaterService(IMovieTheaterRepository movieTheaterRepository, IMapper mapper)
    {
        public async Task<IQueryable<MovieTheater>> GetAllMovieTheatersAysnc()
        {
            var movieTheaters = await movieTheaterRepository.GetAllAsync();
            return movieTheaters;
        }

        public async Task<MovieTheater> GetMovieTheaterByIdAsync(int id)
        {
            return await movieTheaterRepository.GetByIdAsync(id);
        }

        public async Task AddMovieTheaterAsync(MovieTheater movieTheater)
        {
            await movieTheaterRepository.AddAsync(movieTheater);
        }

        public async Task<bool> UpdateMovieTheaterAsync(int id, MovieTheaterDTO movieTheaterDTO)
        {
            if (!await movieTheaterRepository.ExistsAsync(id))
            {
                return false;
            }

            var movieTheater = await movieTheaterRepository.GetByIdAsync(id);
            mapper.Map(movieTheaterDTO, movieTheater);
            await movieTheaterRepository.UpdateAsync(movieTheater);
            await movieTheaterRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMovieTheaterAsync(int id)
        {
            if (!await movieTheaterRepository.ExistsAsync(id))
            {
                return false;
            }
            var movieTheater = await movieTheaterRepository.GetByIdAsync(id);
            await movieTheaterRepository.DeleteAsync(movieTheater);
            await movieTheaterRepository.SaveChangesAsync();
            return true;
        }
        public async Task SaveMovieTheaterChangesAsync()
        {
            await movieTheaterRepository.SaveChangesAsync();
        }
    }
}
