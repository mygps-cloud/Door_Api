using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Register.ViewModels;
using Registration.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models.UserModel;
using WebAPI.Services.UserService;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPI.Services.DeviceService
{
    public class UserFilterService:IUserFilterService
    {
        private IConfiguration config;
        private readonly CommonHelper commonHelper;
        private static UserModel user = new UserModel();
        public UserFilterService(CommonHelper commonHelper, IConfiguration config)
        {
            this.commonHelper = commonHelper;
            this.config = config;
        }
        public async Task<bool> RegisterClient(UserDtoModel User)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(User.Password);
            user.Username = User.Username;
            user.PasswordHash = passwordHash;
            string UserExistQuery = $"SELECT * FROM [UserTable] WHERE Username='{user.Username}'";
            bool userExists = await commonHelper.UserAlreadyExistsAsync(UserExistQuery);
            if (userExists == true)
                return false;
            string Query = $"INSERT INTO [UserTable] (Username, Password) VALUES ('{user.Username}','{user.PasswordHash}')";

            int result =await commonHelper.InsertAsync(Query);
            if(result<0)
                return false;

            return true;
        }
        public async Task<string> LoginClient(UserDtoModel User)
        {
            string UserExistQuery = $"SELECT * FROM [UserTable] WHERE Username='{User.Username}'";
            var Result = await commonHelper.GetPassword(UserExistQuery);

            user.PasswordHash = Result[0].PasswordHash;
            if (Result == null || !BCrypt.Net.BCrypt.Verify(User.Password, user.PasswordHash))
            {
                return string.Empty;
            }
            string token = CreateToken(User);

            return token;
        }


        private string CreateToken(UserDtoModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Token").Value!));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMilliseconds(200), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
