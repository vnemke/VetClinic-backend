using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Case
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Case Name cannot be more than 100 characters")]
        public string Name { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool isPaid { get; set; }
       
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public ICollection<Therapy> Therapies { get; set; }
        public ICollection<Control> Controls { get; set; }
        public ICollection<Xray> Xrays { get; set; }
        public ICollection<VetCase> VetCases { get; set; }
        public ICollection<CasePetService> CasePetServices { get; set; }

        //public Case()
        //{
        //    this.isPaid = false;
        //}

    }
    
   
}
