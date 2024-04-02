﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        //post"/api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult= await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                //add role to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any()) 
                {
                   identityResult= await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered ! Please login");
                    }
                }
                
            }
            return BadRequest("Something went wrong");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user= await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPassword)
                {
                    //Create Token
                    return Ok();
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
