using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class CasePetService
    {        
        public int CaseId { get; set; }
        public Case Case { get; set; }

        public int PetServiceId { get; set; }
        public PetService PetService { get; set; }
    }
}
