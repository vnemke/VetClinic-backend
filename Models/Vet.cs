using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Vet
    {
        private string _firstName;
        private string _lastName;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Vet First Name cannot be more than 50 characters")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value.Trim(); }
        }

        [Required]
        [MaxLength(100, ErrorMessage = "Vet Last Name cannot be more than 100 characters")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value.Trim(); }
        }

        [Required]
        public string IdCard { get; set; }
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }
        public int Phone { get; set; }
        public ICollection<VetCase> VetCases { get; set; }
    }
}
