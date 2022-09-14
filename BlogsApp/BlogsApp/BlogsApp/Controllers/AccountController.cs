using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogsApp.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromBody] Person person)
        {
            var identity = await GetIdentity(person.Login, person.Password);
            if (identity == null)
            {
                return BadRequest(new { error = "Invalid login or password" });
            }

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: "MyServer",
                audience: "MyClient",
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(15)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-mega-puper-key")), SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(string login, string password, string email)
        {
            User user = new User { Email = email, UserName = login };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var response = new
                {
                    login = login,
                    email = email
                };
                return Ok(response);
            }
            return BadRequest();

        }

        [HttpGet("/getuser")]
        public async Task<IActionResult> GetUser(string login)
        {
            User user = await userManager.FindByNameAsync(login);
            if (user != null)
            {
                return Json(user);
            }
            return BadRequest("This user doesn't exist");
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var result = await signInManager.PasswordSignInAsync(login, password, false, false);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, password)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

    }
}
