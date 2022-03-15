using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IPetServiceRepository
    {
        Task<List<PetService>> GetAllPetServices();
        Task<PetService> GetPetService(int petServiceId);

        Task<PetService> AddPetService(PetService entity);
        Task<PetService> UpdatePetService(PetService entity);
        Task<PetService> DeletePetService(int petServiceId);
    }
}
