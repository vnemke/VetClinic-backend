using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Dto
{
    public class XrayDto
    {
        public int Id { get; set; }
        //public IFormFile Files { get; set; }

        public string FileName { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public int CaseId { get; set; }
        //public Case Case { get; set; }
    }
}
