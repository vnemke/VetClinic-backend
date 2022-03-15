using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IVetRepository
    {
        Task<List<Vet>> GetAllVets();
        Task<Vet> GetVet(int vetId);
        Task<List<VetCase>> GetVetsByCase(int caseId);

        Task<Vet> AddVet(Vet entity);
        Task<Vet> UpdateVet(Vet entity);
        Task<Vet> DeleteVet(int vetId);
    }
}
