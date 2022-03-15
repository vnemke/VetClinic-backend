using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class XrayUploader
    {
        public int Id { get; set; }
        public IFormFile Files { get; set; }
        public string Name { get; set; }
    }
}
