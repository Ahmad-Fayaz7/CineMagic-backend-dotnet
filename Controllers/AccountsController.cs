
using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CineMagic.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IMapper mapper) : ControllerBase
    {

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> create([FromBody] UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var result = await userManager.CreateAsync(user, userCredentials.Password);
            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }

            return BadRequest(result.Errors);


        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }

            return BadRequest("Incorrect login");

        }

        [HttpGet("listUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<List<UserDTO>> GetUserList([FromBody] PaginationDTO pagination)
        {
            var usersQueryable = userManager.Users.AsQueryable();
            await HttpContext.InsertPaginationParametersInHeader(usersQueryable);
            var users = await usersQueryable.OrderBy(x => x.Email).Paginate(pagination).ToListAsync();
            return mapper.Map<List<UserDTO>>(users);
        }


        [HttpPost("makeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.AddClaimAsync(user, new Claim("role", "admin"));

            return NoContent();
        }

        [HttpPost("removeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.RemoveClaimAsync(user, new Claim("role", "admin"));

            return NoContent();
        }
        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
        {
            // Claims in payload
            var claims = new List<Claim>
            {
                new Claim("email", userCredentials.Email)
            };

            // Get user
            var user = await userManager.FindByEmailAsync(userCredentials.Email);
            var claimsInDb = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimsInDb);


            // Secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));

            // The key and the algorithm being used
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Expiration time
            var expiration = DateTime.UtcNow.AddYears(1);

            // combines all the parts
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), // writes out the jwt token
                Expiration = expiration,
            };
        }

    }
}
