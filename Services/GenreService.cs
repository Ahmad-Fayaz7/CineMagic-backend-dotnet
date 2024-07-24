using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class GenreService(IGenreRepository repository, IMapper mapper)
    {
        private readonly IGenreRepository _repository = repository;

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Genre genre)
        {
            await _repository.AddAsync(genre);
        }

        public async Task<bool> UpdateGenreAsync(int id, GenreCreationDTO genreCreationDto)
        {
            if (!await _repository.ExistsAsync(id))
            {
                return false;
            }

            var genre = await _repository.GetByIdAsync(id);
            mapper.Map(genreCreationDto, genre);
            await _repository.UpdateAsync(genre);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
            {
                return false;
            }
            var genre = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(genre);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
