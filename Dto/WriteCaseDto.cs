using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class WriteCaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Diagnosis { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/y}")]
        public string Date { get; set; }
        public string Description { get; set; }
        public int PetId { get; set; }

        public ICollection<WriteVetCaseDto> VetCases { get; set; }
        public ICollection<WriteCasePetServiceDto> CasePetServices { get; set; }
        public ICollection<TherapyDto> Therapies { get; set; }
        public ICollection<ControlDto> Controls { get; set; }
        public ICollection<XrayDto> Xrays { get; set; }
    }
}
