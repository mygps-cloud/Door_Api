using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using Register.ViewModels;
using Registration.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _config;
        CommonHelper _Helper;
        public static UserModel user = new UserModel();
        public AccountController(IConfiguration config)
        {
            _config = config;
            _Helper = new CommonHelper(_config);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register")]
        public ActionResult<UserDtoModel> Register(UserDtoModel Vm)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(Vm.Password);
            user.Username = Vm.Username;
            user.PasswordHash = passwordHash;
            string UserExistQuery = $"SELECT * FROM [UserTable] WHERE Username='{user.Username}'";
            bool userExists = _Helper.UserAlreadyExists(UserExistQuery);
            if (userExists == true)
            {
                return BadRequest("Username and Email Already Exists Try Other");
            }
            string Query = $"INSERT INTO [UserTable] (Username, Password) VALUES ('{user.Username}','{user.PasswordHash}')";

            int result = _Helper.DMLTransaction(Query);
            if (result < 0)
            {
                return BadRequest();
            }
            return Ok("Thanks for Reistration");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("Login")]

        public ActionResult<UserDtoModel> Login(UserDtoModel Vm)
        {
            string UserExistQuery = $"SELECT * FROM [UserTable] WHERE Username='{Vm.Username}'";
            var Result = _Helper.GetPassword(UserExistQuery);

            user.PasswordHash = Result[0].PasswordHash;
            if (Result == null || !BCrypt.Net.BCrypt.Verify(Vm.Password, user.PasswordHash))
            {
                return BadRequest("User or Password not correct");
            }
            string token = CreateToken(Vm);
            return Ok(token);
        }
        private string CreateToken(UserDtoModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Token").Value!));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMilliseconds(200), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void EntryIntoSession(string username)
        {
            HttpContext.Session.SetString("Username", username);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
