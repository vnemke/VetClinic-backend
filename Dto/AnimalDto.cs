using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public ICollection<Race> Races { get; set; }
        //public ICollection<RaceDto> Races { get; set; }
        //public ICollection<PetDto> Pets { get; set; }
    }
}
