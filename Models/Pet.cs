using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Pet Name cannot be more than 20 characters")]
        public string Name { get; set; }

        [Required]
        public Gender Sex { get; set; }

        [Required]
        public DateTime Year { get; set; }

        [Required]
        [ForeignKey("Race")]
        public int RaceId { get; set; }
        public Race Race { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public ICollection<Case> Cases { get; set; }

        public enum Gender { female, male }

    }
}
