using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class PetCase
    {
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
