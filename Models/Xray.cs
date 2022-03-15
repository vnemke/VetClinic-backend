using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Xray
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FileName { get; set; }

        //[Required]
        public string Url { get; set; }

        //[Required]
        public DateTime Date { get; set; }


        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public virtual Case Case { get; set; }
    }
}
