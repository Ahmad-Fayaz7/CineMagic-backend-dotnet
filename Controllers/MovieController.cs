using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Helpers;
using CineMagic.Models;
using CineMagic.Repositories.IRepositories;
using CineMagic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineMagic.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController(MovieService movieService, IMapper mapper, IFileStorageService inAppStorageService, GenreService genreService, MovieTheaterService movieTheaterService, ActorService actorService, IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly string containerName = "movies";

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movie = mapper.Map<Movie>(movieCreationDTO);
            if (movieCreationDTO.Poster != null)
            {
                movie.Poster = await inAppStorageService.SaveFile(containerName, movieCreationDTO.Poster);
            }
            AddOrderToMovie(movie);
            await movieService.AddMovieAsync(movie);

            return movie.Id;
        }
        [HttpGet("GetPost")]
        public async Task<ActionResult<MovieGetPostDTO>> GetPost()
        {
            var genres = await genreService.GetAllAsync();
            genres = genres.OrderBy(x => x.Name);
            var movieTheaters = await movieTheaterService.GetAllMovieTheatersAysnc();
            movieTheaters = movieTheaters.OrderBy(x => x.Name);
            var genresDto = mapper.Map<List<GenreDTO>>(genres);
            var movieTheatersDto = mapper.Map<List<MovieTheaterDTO>>(movieTheaters);
            var movieGetPostDto = new MovieGetPostDTO { Genres = genresDto, MovieTheaters = movieTheatersDto };

            return movieGetPostDto;
        }

        [HttpGet]
        public async Task<ActionResult<HomeDTO>> Get()
        {
            var inTheater = await movieService.GetMovieshInTheaters();
            var inTheaterDto = mapper.Map<List<MovieDTO>>(inTheater);
            var upcomingReleases = await movieService.GetUpcomingMovies();
            var ucomingReleasesDto = mapper.Map<List<MovieDTO>>(upcomingReleases);
            var homeDto = new HomeDTO { InTheaters = inTheaterDto, UpcomingReleases = ucomingReleasesDto };
            return homeDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await movieService.GetMovieWithDetails(id);
            if (movie == null)
            {
                return NotFound();
            }
            var movieDto = mapper.Map<MovieDTO>(movie);
            movieDto.Actors = movieDto.Actors.OrderBy(x => x.Order).ToList();
            return movieDto;
        }

        [HttpGet("putget/{id:int}")]
        public async Task<ActionResult<MoviePutGetDTO>> PutGet(int id)
        {
            var movieActionResult = Get(id);
            if (movieActionResult.Result is NotFoundResult) { return NotFound(); }
            var movie = movieActionResult.Result.Value;
            var selectedGenresIds = movie.Genres.Select(x => x.Id).ToList();
            var nonSelectedGenres = await genreService.GetNonSelectedGenres(selectedGenresIds);

            var selectedMovieTheatersIds = movie.MovieTheaters.Select(x => x.Id).ToList();
            var nonSelectedMovieTheaters = await movieTheaterService.GetNonSelectedMovieTheaters(selectedMovieTheatersIds);

            var nonSelectedGenresDto = mapper.Map<List<GenreDTO>>(nonSelectedGenres);
            var nonSelectedMovieTheaterDto = mapper.Map<List<MovieTheaterDTO>>(nonSelectedMovieTheaters);

            var response = new MoviePutGetDTO();
            response.Movie = movie;
            response.SelectedGenres = movie.Genres;
            response.NonSelectedGenres = nonSelectedGenresDto;
            response.SelectedMovieTheaters = movie.MovieTheaters;
            response.NonSelectedMovieTheaters = nonSelectedMovieTheaterDto;
            response.Actors = movie.Actors;

            return response;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> Filter([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            try
            {
                var moviesQueryable = await movieService.GetAllMoviesAsQueryableAsync();

                // Title filter
                if (!string.IsNullOrEmpty(movieFilterDTO.Title))
                {
                    moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(movieFilterDTO.Title));
                }

                // In Theaters filter
                if (movieFilterDTO.InTheaters)
                {
                    moviesQueryable = moviesQueryable.Where(x => x.InTheaters);
                }

                // Upcoming Releases filter
                if (movieFilterDTO.UpcomingReleases)
                {
                    var today = DateTime.Today;
                    moviesQueryable = moviesQueryable.Where(x => x.ReleaseDate > today);
                }

                // Genre filter
                if (movieFilterDTO.GenreId != 0)
                {
                    moviesQueryable = moviesQueryable.Where(x => x.MovieGenres.Any(y => y.GenreId == movieFilterDTO.GenreId));
                }

                // Passes the total amount of elements/records to front-end
                await HttpContext.InsertPaginationParametersInHeader(moviesQueryable);

                // Paginates the filter result
                var movies = await moviesQueryable.OrderBy(x => x.Title).Paginate(movieFilterDTO.paginationDTO).ToListAsync();

                return mapper.Map<List<MovieDTO>>(movies);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                // _logger.LogError(ex, "An error occurred while filtering movies");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movie = await movieService.GetMovieWithDetails(id);
            if (movie == null)
            {
                return NotFound();
            }
            movie = mapper.Map(movieCreationDTO, movie);
            if (movieCreationDTO.Poster != null)
            {
                movie.Poster = await inAppStorageService.EditFile(containerName, movieCreationDTO.Poster, movie.Poster);
            }

            AddOrderToMovie(movie);
            await unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            await movieService.DeleteMovie(movie);
            await unitOfWork.CompleteAsync();
            await inAppStorageService.DeleteFile(containerName, movie.Poster);
            return NoContent();
        }
        private void AddOrderToMovie(Movie movie)
        {
            if (movie.MovieActors != null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i;
                }
            }

        }
    }
}
