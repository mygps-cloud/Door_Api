using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models.ViewModel;
using WebAPI.Services.UserService;

namespace WebAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class AccountController : Controller
    {
	    #region UserServiceConfig
		private readonly IUserService userService;
        public AccountController( IUserService userService)
        {
            this.userService = userService;
        }
        #endregion
        #region RegisterEndPoint
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Register([FromBody]UserVM User)
		{
			try
			{
				await userService.RegisterClient(User);
				return Created(nameof(User)," User Created Successful");
			}
			catch (ArgumentNullException e)
			{
				return BadRequest(e.Message);
			}
			catch (ArgumentException e)
			{
				return Conflict(e.Message);
			}
			catch (DbUpdateException e)
			{
				return StatusCode(500,e.Message);
			}
		}
        #endregion
        #region LoginEndPoint
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody]UserVM User)
        {
	        try
	        {
		        var Result = await userService.LoginClient(User);
		        return Ok(new
		        {
			        Result,
			        durationTime = DateTime.Now.AddDays(10)
		        });
			}

	        catch (ArgumentNullException e)
	        {
		        return BadRequest(e.Message);
	        }
	        catch (ArgumentException e)
	        {
		        return Conflict(e.Message);
	        }
		}

			
	}
        #endregion
}
