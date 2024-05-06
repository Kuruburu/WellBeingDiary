using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using WellBeingDiary.Dtos.Account;
using WellBeingDiary.Entities;

namespace WellBeingDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
             _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.EmailAddress,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (!createdUser.Succeeded) 
                    return StatusCode(500, createdUser.Errors);
                
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                if(!roleResult.Succeeded)
                    return StatusCode(500, roleResult.Errors);

                return Created();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}
