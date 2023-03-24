﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Register.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.Models.UserModel;
using WebAPI.Services.UserService;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPI.Services.DeviceService
{
    public class UserFilterService:IUserFilterService
    {

	    private IConfiguration config;
        private readonly DataAccessUser commonHelper;
        private static UserModel user = new UserModel();
        int[] RequestCodes = { 400, 409, 500, 201 };
		public UserFilterService(DataAccessUser commonHelper, IConfiguration config)
        {
            this.commonHelper = commonHelper;
            this.config = config;
        }

        public async Task<int> RegisterClient(UserDtoModel User)
        {
	        if (string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.Username))
	        {
		        return RequestCodes[0];
	        }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(User.Password);
            user.Username = User.Username;
            user.PasswordHash = passwordHash;
            string UserExistQuery = $"SELECT * FROM [UserTable] WHERE Username='{user.Username}'";
            bool userExists = await commonHelper.UserAlreadyExistsAsync(UserExistQuery);
            if (userExists == true)
                return RequestCodes[1];
            string Query = $"INSERT INTO [UserTable] (Username, Password) VALUES ('{user.Username}','{user.PasswordHash}')";

            int result =await commonHelper.InsertAsync(Query);
            if(result<0)
                return RequestCodes[2];

            return RequestCodes[3];
        }

        public async Task<string> LoginClient(UserDtoModel User)
        {
			if (string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.Username))
			{
				return RequestCodes[0].ToString();
			}//Cintinue more Request Logic
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
