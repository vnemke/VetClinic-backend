using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class VetCase
    {
        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
