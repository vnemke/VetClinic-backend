using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAnimals(/*string name*/);
        Task<Animal> GetAnimal(int animalId);

        Task<Animal> AddAnimal(Animal entity);
        Task<Animal> UpdateAnimal(Animal entity);
        Task<Animal> DeleteAnimal(int animalId);       
    }
}
