using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieTheaterMovie> MovieTheaterMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(x => new { x.MovieId, x.GenreId });
            modelBuilder.Entity<MovieActor>().HasKey(x => new { x.MovieId, x.ActorId });
            modelBuilder.Entity<MovieTheaterMovie>().HasKey(x => new { x.MovieId, x.MovieTheaterId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
