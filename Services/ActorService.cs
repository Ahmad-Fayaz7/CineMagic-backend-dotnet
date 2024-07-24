using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Services
{
    public class ActorService(IActorRepository actorRepository, IMapper mapper)
    {
        public async Task<IQueryable<Actor>> GetAllActorsAysnc()
        {
            var actors = await actorRepository.GetAllAsync();
            return actors;
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await actorRepository.GetByIdAsync(id);
        }

        public async Task AddActorAsync(Actor actor)
        {
            await actorRepository.AddAsync(actor);
        }

        public async Task<bool> UpdateActorAsync(int id, ActorCreationDTO actorCreationDTO)
        {
            if (!await actorRepository.ExistsAsync(id))
            {
                return false;
            }

            var actor = await actorRepository.GetByIdAsync(id);
            mapper.Map(actorCreationDTO, actor);
            await actorRepository.UpdateAsync(actor);
            await actorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteActorAsync(int id)
        {
            if (!await actorRepository.ExistsAsync(id))
            {
                return false;
            }
            var actor = await actorRepository.GetByIdAsync(id);
            await actorRepository.DeleteAsync(actor);
            await actorRepository.SaveChangesAsync();
            return true;
        }
        public async Task SaveActorChangesAsync()
        {
            await actorRepository.SaveChangesAsync();
        }
    }
}
