using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class GetVetCaseDto
    {
        public int CaseId { get; set; }

        public int VetId { get; set; }
        public VetDto Vet { get; set; }
    }
}
