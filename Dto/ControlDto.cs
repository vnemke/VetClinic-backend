using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class ControlDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/y}")]
        public string Date { get; set; }
        public string Description { get; set; }

        //public int CaseId { get; set; }
        //public GetCaseDto Case { get; set; }
    }
}
