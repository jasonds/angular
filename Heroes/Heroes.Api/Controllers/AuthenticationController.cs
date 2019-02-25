using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Heroes.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Heroes.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            AppSettings appSettings)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        // POST api/authentication/generate
        [AllowAnonymous]
        [HttpPost]
        [Route("generate")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        ////var claims = new[]
                        ////{
                        ////    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        ////    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        ////};

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Security.Tokens.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Issuer = _appSettings.Security.Tokens.Issuer,
                            Audience = _appSettings.Security.Tokens.Audience,
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, user.Id)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(30),
                            SigningCredentials = creds
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);

                        return Ok(new
                        {
                            Id = user.Id,
                            Username = user.Email,
                            Token = tokenString
                        });
                    }
                }
            }

            return BadRequest("Could not create token");
        }
    }
}
