using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class RaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AnimalId { get; set; }
        public AnimalDto Animal { get; set; }
    }
}
