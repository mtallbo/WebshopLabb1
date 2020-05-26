using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.OrderAPI.Models;

namespace Webshop.OrderAPI.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User Authenticate(AuthenticateModel model);
        User Register(AuthenticateModel model);
    }
}
