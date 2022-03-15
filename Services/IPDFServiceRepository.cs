using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IPDFServiceRepository
    {
        Task<Boolean> AddPDFCase(int caseId);
    }
}
