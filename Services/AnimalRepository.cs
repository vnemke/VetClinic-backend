using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _animalContext;

        public AnimalRepository(ApplicationDbContext animalContext)
        {
            _animalContext = animalContext;
        }
        public async Task<Animal> AddAnimal(Animal animal)
        {
            _animalContext.Set<Animal>().Add(animal);
            await _animalContext.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> DeleteAnimal(int animalId)
        {
            var entity = await _animalContext.Set<Animal>().FindAsync(animalId);
            if(entity == null)
            {
                return entity;
            }

            _animalContext.Set<Animal>().Remove(entity);
            await _animalContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Animal>> GetAllAnimals(/*string name*/)
        {
            IQueryable<Animal> query = _animalContext.Set<Animal>();
                //.Where(a => a.Name == name);

            return await query.ToListAsync();
        }
         
        public async Task<Animal> GetAnimal(int animalId)
        {
            IQueryable<Animal> query = _animalContext.Set<Animal>()
                .Where(a => a.Id == animalId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Animal> UpdateAnimal(Animal animal)
        {
            _animalContext.Update(animal);
            await _animalContext.SaveChangesAsync();
            return animal;
        }
    }

}
