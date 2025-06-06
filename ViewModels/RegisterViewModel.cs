using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UDAS.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string RoleId { get; set; }
        
    }
}