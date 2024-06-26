﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using WellBeingDiary.Dtos.Account;
using WellBeingDiary.Entities;
using WellBeingDiary.Interfaces;

namespace WellBeingDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserRepository userRepo, ITokenService tokenService)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>GetAll() 
        {
            var users = await _userRepo.GetAllAsync();
            
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var me = await _userRepo.GetMeAsync();

            if (me is null)
                return NotFound();

            return Ok(me);
        }

        [HttpGet("me/id")]
        [Authorize]
        public IActionResult GetMyId()
        {
            var myId = _userRepo.GetMyId();

            if (string.IsNullOrEmpty(myId))
                return NotFound();

            return Ok(myId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username!.ToLower());

            if (user is null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            
            var token = await _tokenService.CreateToken(user);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = token
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

                if (!createdUser.Succeeded)
                    return StatusCode(500, createdUser.Errors);

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                if (!roleResult.Succeeded)
                    return StatusCode(500, roleResult.Errors);

                var token = await _tokenService.CreateToken(appUser);
                _tokenService.SetTokenCookie(HttpContext, token);

                return Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = token
                    }
                );
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var user = await _userRepo.DeleteAsync(id);

            if (user is null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("me")]
        [Authorize]
        public async Task<IActionResult> DeleteMe()
        {
            var myId = _userRepo.GetMyId();

            if (string.IsNullOrEmpty(myId))
                return Unauthorized();

            await _userRepo.DeleteAsync(myId);

            return NoContent();
        }
    }
}