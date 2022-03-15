using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class PetWithOwner
    {
        public Pet Pet { get; set; }
        public Owner Owner { get; set; }
    }
}
