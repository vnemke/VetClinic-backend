using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Owner First Name cannot be more than 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Owner Last Name cannot be more than 60 characters")]
        public string LastName { get; set; }

        [Required]
        public string IdCard { get; set; }
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Phone { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
