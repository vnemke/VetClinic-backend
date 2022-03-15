using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class TherapyDto
    {
        
        public int Id { get; set; }
        public string Drug { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/y}")]
        public string Date { get; set; }

        public int CaseId { get; set; }
        public GetCaseDto Case { get; set; }
    }
}
