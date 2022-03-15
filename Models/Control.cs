using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Control
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Control Name cannot be more than 100 characters")]
        public string Name { get; set; }

        //[Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }

        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
