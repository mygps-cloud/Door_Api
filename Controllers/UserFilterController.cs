using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using Register.ViewModels;
using Registration.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models.UserModel;
using WebAPI.Services.UserService;

namespace WebAPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserFilterService userService;
        public AccountController( IUserFilterService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDtoModel User)
        {
            var Results = await userService.RegisterClient(User); 
            if (Results == false)
                return BadRequest("User Already Existed");

            return Created(nameof(User),"Registartion Successful");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDtoModel User)
        {
            var Result=await userService.LoginClient(User);
            if (string.IsNullOrEmpty(Result))
                return BadRequest("Username or Password incorrect");
            return Ok(Result);
        }
    }
}
