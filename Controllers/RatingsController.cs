
using CineMagic.DTOs;
using CineMagic.Models;
using CineMagic.Repositories;
using CineMagic.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingsController(MovieService movieService, UserManager<IdentityUser> userManager, UnitOfWork unitOfWork) : ControllerBase
    {


        [HttpPost]
        // The user should be authorized for rating
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingDTO ratingDTO)
        {

            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;
            var currentRate = await movieService.GetCurrentRate(userId, ratingDTO.MovieId);
            if (currentRate == null)
            {
                var rating = new Rating()
                {
                    UserId = userId,
                    MovieId = ratingDTO.MovieId,
                    Rate = ratingDTO.Rate,
                };
                await movieService.AddRatingAsync(rating);
            }
            else
            {
                currentRate.Rate = ratingDTO.Rate;
            }
            await unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
