using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class GenreService(IUnitOfWork unitOfWork, IMapper mapper)
    {


        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await unitOfWork.Genres.GetAllAsync();


        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await unitOfWork.Genres.GetByIdAsync(id);
        }

        public async Task AddAsync(Genre genre)
        {
            await unitOfWork.Genres.AddAsync(genre);
        }

        public async Task<bool> UpdateGenreAsync(int id, GenreCreationDTO genreCreationDto)
        {
            if (!await unitOfWork.Genres.ExistsAsync(id))
            {
                return false;
            }

            var genre = await unitOfWork.Genres.GetByIdAsync(id);
            mapper.Map(genreCreationDto, genre);
            await unitOfWork.Genres.UpdateAsync(genre);

            return true;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            if (!await unitOfWork.Genres.ExistsAsync(id))
            {
                return false;
            }
            var genre = await unitOfWork.Genres.GetByIdAsync(id);
            await unitOfWork.Genres.RemoveAsync(genre);

            return true;
        }
    }
}
