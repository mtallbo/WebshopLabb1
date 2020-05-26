using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webshop.OrderAPI.Helpers;
using Webshop.OrderAPI.Models;

namespace Webshop.OrderAPI.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User> 
        {
            new User { Id = Guid.NewGuid(), Email = "Test", Password = "Test"}
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSetttings)
        {
            _appSettings = appSetttings.Value;
        }
        
        public User Authenticate(string email, string password)
        {
            var user = _users.SingleOrDefault(u => u.Email == email && u.Password == password);
            if(user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public List<User> GetUsers()
        {
            return _users;
        }

    }
}
