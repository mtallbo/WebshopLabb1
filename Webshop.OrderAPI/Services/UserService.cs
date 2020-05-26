using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
using Webshop.OrderAPI.Models;

namespace Webshop.OrderAPI.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User> 
        {
            new User { Id = Guid.NewGuid(), Email = "Test", Password = "Test"}
        };

        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public User Authenticate(AuthenticateModel model)
        {
            var user = _users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if(user == null)
            {
                return null;
            }
            var token = GenerateToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public User Register(AuthenticateModel model)
        {
            if (model == null)
            {
                return null;
            };
            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Password = model.Password
            };
            var existingUser = _users.SingleOrDefault(u => u.Email == newUser.Email && u.Password == newUser.Password);
            if(existingUser == null)
            {
                return null;
            }
            _users.Add(newUser);
            return newUser;
        }

        public List<User> GetAll()
        {
            return _users;
        }

        private SecurityToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Appsettings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
