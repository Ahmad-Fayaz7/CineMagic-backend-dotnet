using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Services
{
    public class ActorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<IEnumerable<Actor>> GetAllActorsAysnc()
        {
            return await unitOfWork.Actors.GetAllAsync();
        }

        public async Task<IQueryable<Actor>> GetActorsAsQueryable()
        {
            return await unitOfWork.Actors.GetActorsAsQueryable();
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await unitOfWork.Actors.GetByIdAsync(id);
        }

        public async Task AddActorAsync(Actor actor)
        {
            await unitOfWork.Actors.AddAsync(actor);
        }

        public async Task<bool> UpdateActorAsync(int id, ActorCreationDTO actorCreationDTO)
        {
            if (!await unitOfWork.Actors.ExistsAsync(id))
            {
                return false;
            }

            var actor = await unitOfWork.Actors.GetByIdAsync(id);
            mapper.Map(actorCreationDTO, actor);


            return true;
        }

        public async Task<bool> DeleteActorAsync(int id)
        {
            if (!await unitOfWork.Actors.ExistsAsync(id))
            {
                return false;
            }
            var actor = await unitOfWork.Actors.GetByIdAsync(id);
            await unitOfWork.Actors.RemoveAsync(actor);

            return true;
        }

        public async Task<ActionResult<IEnumerable<MovieActorDTO>>> GetActorsByName(string name)
        {
            return await unitOfWork.Actors.GetActorsByName(name);
        }
    }
}
