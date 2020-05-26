using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.OrderAPI.Models;

namespace Webshop.OrderAPI.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User Authenticate(string email, string password);
    }
}
