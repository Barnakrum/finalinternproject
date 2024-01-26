using finalinternshipproject.Data;
using finalinternshipproject.Dtos.User;
using finalinternshipproject.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace finalinternshipproject.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContext;


        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.configuration = configuration;
            this.httpContext = httpContext;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await context.Users
                .FirstOrDefaultAsync(u => u.Name.ToLower().Equals(username.ToLower()));

            if(user == null || user.isBlocked == true || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Message = "Wrong password or username";
                response.Success = false;
            }
            else
            {

                response.Data = CreateToken(await context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower()));
            }


            return response;
        }

        public async Task<ServiceResponse<int>> Register(string username, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if(await UserExists(username))
            {
                response.Message = "User already exists";
                response.Success = false;
                return response;
            }




            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User { Name = username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt};
            context.Users.Add(user);
            await context.SaveChangesAsync();
            response.Data = user.Id;


            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await context.Users.AnyAsync(u => u.Name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateToken(User user)
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var key = System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);



            return stringToken;
        }

        public async Task<ServiceResponse<string>> UserSession()
        {
            var response = new ServiceResponse<string>();

            response.Data = GetUserName();

            if (response.Data == null) response.Success = false;
            return response;
        }

        public string GetUserName()
        {
            return httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        public ServiceResponse<int> GetUserId()
        {
            var response = new ServiceResponse<int>
            {
                Data = int.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            return response;
        }
    }
}
