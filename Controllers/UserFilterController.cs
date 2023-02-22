using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using Register.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models.UserModel;
using WebAPI.Services.UserService;

namespace WebAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserFilterService userService;
        public AccountController( IUserFilterService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDtoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]UserDtoModel User)
        {
            var Results = await userService.RegisterClient(User); 
            if (Results == false)
                return BadRequest("User Already Existed");

            return Created(nameof(User),"Registartion Successful");
        }
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDtoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody]UserDtoModel User)
        {
            var Result=await userService.LoginClient(User);
            if (string.IsNullOrEmpty(Result))
                return BadRequest("Username or Password incorrect");
            return Ok(Result);
        }
    }
}
