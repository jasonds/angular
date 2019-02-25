using System;
using System.Threading.Tasks;
using Heroes.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // POST api/user/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return BadRequest("User already exists");
            }

            var result = await _userManager.CreateAsync(new IdentityUser(model.Email), model.Password);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Email = model.Email
                });
            }
            else
            {
                return BadRequest("Could not create token");
            }
        }
    }
}
