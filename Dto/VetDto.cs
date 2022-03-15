using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class VetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string IdCard { get; set; }
        public string Address { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        public int Phone { get; set; }
        //public ICollection<VetCase> VetCases { get; set; }
    }
}
