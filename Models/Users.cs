using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UDAS.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}