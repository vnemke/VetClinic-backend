using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Dto
{
    public class WritePetDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Gender Sex { get; set; }

        [Required]
        public DateTime Year { get; set; }

        [Required]
        public int RaceId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public enum Gender
        {
            female,
            male
        }
    }
}
