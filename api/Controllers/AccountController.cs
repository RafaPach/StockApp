using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;


namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUSer> _userManager
        private readonly ITokenService _tokenservice

        private readonly SigningManager _signingManager
        public AccountController(UserManager<AppUser>  userManager, ITokenService tokenservice, SigningManager<AppUser> signingManager)
        {
            _userManager = userManager;
            _tokenservice = tokenservice;
            _signingManager = signingManager;
        }

        
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var user = await _userManager.Users.FirstOrDefault(x=> x.UserName == loginDto.UserName.Lower());

                if(user == null) return Unauthorized("Invalid username!");

                var result = await _signingManager.CheckPassowrdSignInAsync(user, loginDto.Password, false);
            
                if(!result.Sucessed) return Unauthorized("UserName not found and/or pw incorrect");

                return Ok(new NewUserDto {
                    UserName =  user.UserName;
                    Email =  user.Email;
                    Token =  _tokenservice.CreateToken(user);
                })
            
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {

            try
            {
                if(!ModelState.IsValid)
                return BadRequest(ModelState)

                var appUser = new AppUser{

                    UserName =  registerDto.UserName;
                    Email = registerDto.Email;

                }

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password)

                if(createdUser.Sucessed){
                    var roleResult = await _userManager.AddRoleAsync(appUser, "User");

                    // Anyone that signs through the register end point will be added the User Role
                     if(roleResult.Sucessed){
                    
                    return Ok(new NewUserDto {
                        UserName = appUser.UserName,
                        Email =  appUser.Email,
                        Token = _tokenservice.CreateToken(appUser)
                    })
                }   else {
                    return StatusCode(500, roleResult.Errors);
                    }
                }

                else{
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (System.Exception e)
            {
                
                return StatusCode(500, e);
            }
        }
    }
}