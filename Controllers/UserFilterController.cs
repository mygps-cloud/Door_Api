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
        #region UserServiceConfig
        private readonly IUserFilterService userService;
        public AccountController( IUserFilterService userService)
        {
            this.userService = userService;
        }
        #endregion
        #region RegisterEndPoint
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDtoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Register([FromBody]UserDtoModel User)
		{
			int[] RequestCodes = { 400, 409, 500, 201 };
			var Results = await userService.RegisterClient(User); 
            if (Results == RequestCodes[1])
                return Conflict("User Already Existed");

            if(Results == RequestCodes[0])
               return BadRequest("Inccorect Parameter");

            if(Results== RequestCodes[2])
				return StatusCode(StatusCodes.Status500InternalServerError);

			return Created(nameof(User),"Registartion Successful");
        }
        #endregion
        #region LoginEndPoint
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
        #endregion
    }
}
