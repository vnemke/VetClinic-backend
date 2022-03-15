using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class GetCaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Diagnosis { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yy}")]
        public string Date { get; set; }
        public string Description { get; set; }
        public int PetId { get; set; }
        public GetPetDto Pet { get; set; }
        public bool IsPaid { get; set; }
        public ICollection<GetVetCaseDto> VetCases { get; set; }
        public ICollection<GetCasePetServiceDto> CasePetServices { get; set; }
        public ICollection<TherapyDto> Therapies { get; set; }
        public ICollection<ControlDto> Controls { get; set; }
        public ICollection<XrayDto> Xrays { get; set; }

    }

}
