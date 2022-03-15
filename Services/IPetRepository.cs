using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllPets();
        Task<Pet> GetPet(int petId);
        Task<List<Pet>> GetPetsByAnimal(int animalId);
        Task<List<Pet>> GetPetsByOwner(int ownerId);
        Task<List<Pet>> GetPetsByDate();

        Task<Pet> AddPet(Pet entity);
        Task<Pet> UpdatePet(Pet entity);
        Task<Pet> DeletePet(int petId);
    }
}
