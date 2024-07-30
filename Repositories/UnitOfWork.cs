using CineMagic.Models;
using CineMagic.Repositories.IRepositories;

namespace CineMagic.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IActorRepository Actors { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IMovieRepository Movies { get; private set; }

        public IMovieTheaterRepository MoviesTheaters { get; private set; }
        private readonly ApplicationDbContext _context;


        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            Actors = new ActorRepository(_context);
            Genres = new GenreRepository(_context);
            Movies = new MovieRepository(_context);
            MoviesTheaters = new MovieTheaterRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
