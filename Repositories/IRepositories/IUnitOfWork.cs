﻿namespace CineMagic.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IActorRepository Actors { get; }
        IGenreRepository Genres { get; }
        IMovieRepository Movies { get; }
        IMovieTheaterRepository MoviesTheaters { get; }
        IRatingRepository Rates { get; }
        Task<int> CompleteAsync();
    }
}
