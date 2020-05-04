using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
