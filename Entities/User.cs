using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace UDAS.Entities
{
    public class User
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }

        public string? PasswordHash { get; set; }
        public string? RoleId { get; set; }
        
    }
}