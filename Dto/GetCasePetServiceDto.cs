﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class GetCasePetServiceDto
    {
        public int CaseId { get; set; }

        public int PetServiceId { get; set; }
        public PetServiceDto PetService { get; set; }
    }
}
