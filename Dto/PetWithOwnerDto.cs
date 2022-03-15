using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class PetWithOwnerDto
    {
        public WritePetDto Pet { get; set; }
        public OwnerDto Owner { get; set; }
    }
}
