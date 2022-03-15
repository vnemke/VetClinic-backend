using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
