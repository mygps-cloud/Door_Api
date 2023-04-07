using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.Models.UserModel;
using WebAPI.Models.ViewModel;
using WebAPI.Services.UserService;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPI.Services.DeviceService
{
    public class UserService: IUserService
	{
	    private readonly IConfiguration _config;
		private readonly AppDbContext _context;

		public UserService(AppDbContext context,IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		public async Task<bool> RegisterClient(UserVM user)
       {
	        UserModel userTdoModel = new UserModel();
			if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
	        {
		        throw new ArgumentNullException();
	        }


			string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

			userTdoModel.Username = user.Username;
			userTdoModel.PasswordHash = passwordHash;

			bool userExists = _context.User.Any(u => u.Username == user.Username);
			if (userExists)
			{
				throw new ArgumentException("Username already exists");
			}
			await _context.User.AddAsync(userTdoModel);
			if (await _context.SaveChangesAsync() == 0)
				throw new DbUpdateException("Error Save user");

			return true;
       }

        public async Task<string> LoginClient(UserVM user)
        {
			if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
			{
				throw new ArgumentNullException();
			}

			string passwordHash =await _context.User
				.Where(u => u.Username == user.Username).Select(u => u.PasswordHash).FirstOrDefaultAsync();


			if (!BCrypt.Net.BCrypt.Verify(user.Password, passwordHash))
			{
				throw new ArgumentException("Invalid password");
			}

			string token = CreateToken(user);

            return token;
        }


        private string CreateToken(UserVM user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Token").Value!);
			var jwt = new JwtSecurityToken(
				claims: claims,
				notBefore: DateTime.UtcNow,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
			return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
