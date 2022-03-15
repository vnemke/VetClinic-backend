using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Race
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Race Name cannot be more than 20 characters")]
        public string Name { get; set; }

        [ForeignKey("Animal")]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
