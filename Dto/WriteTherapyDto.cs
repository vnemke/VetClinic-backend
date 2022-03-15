using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class WriteTherapyDto
    {
        public int Id { get; set; }
        public string Drug { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int CaseId { get; set; }

    }
}
