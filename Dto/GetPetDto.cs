using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class GetPetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Gender Sex { get; set; }

        public DateTime Year { get; set; }

        public int RaceId { get; set; }
        public RaceDto Race { get; set; }

        public int OwnerId { get; set; }
        public OwnerDto Owner { get; set; }

        public enum Gender
        {
            female,
            male
        }
    }
}
