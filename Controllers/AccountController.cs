using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController: ControllerBase
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var appUser = new AppUser {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if(createdUser.Succeeded) {
                    var  roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)

                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {
                
                return StatusCode(500, e);
            }
        }

    }
}