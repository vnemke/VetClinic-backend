using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _petContext;
        private readonly ApplicationDbContext _ownerContext;

        public PetRepository(ApplicationDbContext petContext, ApplicationDbContext ownerContext)
        {
            _petContext = petContext;
            _ownerContext = ownerContext;
        }
        public async Task<Pet> AddPet(Pet pet)
        {
            _petContext.Set<Pet>().Add(pet);
            await _petContext.SaveChangesAsync();
            return pet;
        }

        public async Task<Pet> DeletePet(int petId)
        {
            var entity = await _petContext.Set<Pet>().FindAsync(petId);
            if (entity == null)
            {
                return entity;
            }

            _petContext.Set<Pet>().Remove(entity);
            await _petContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Pet>> GetAllPets()
        {
            IQueryable<Pet> query = _petContext.Set<Pet>()
                .Include(r => r.Race).ThenInclude(a => a.Animal)
                .Include(o => o.Owner);
                
            return await query.ToListAsync();
        }

        public async Task<Pet> GetPet(int petId)
        {
            IQueryable<Pet> query = _petContext.Set<Pet>()
               .Where(p => p.Id == petId)
               .Include(r => r.Race).ThenInclude(a => a.Animal)
               .Include(o => o.Owner);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Pet>> GetPetsByAnimal(int animalId)
        {
            IQueryable<Pet> query = _petContext.Set<Pet>()
                .Where(p => p.Race.AnimalId == animalId)
                .Include(r => r.Race).ThenInclude(a => a.Animal)
                .Include(o => o.Owner);

            return await query.ToListAsync();
        }

        public async Task<List<Pet>> GetPetsByDate()
        {
            IQueryable<Pet> query = _petContext.Set<Pet>()
               .OrderByDescending(p => p.Year)
               .Include(r => r.Race).ThenInclude(a => a.Animal)
               .Include(o => o.Owner);

            return await query.ToListAsync();
        }

        public async Task<List<Pet>> GetPetsByOwner(int ownerId)
        {
            IQueryable<Pet> query = _petContext.Set<Pet>()
               .Where(o => o.OwnerId == ownerId)
               .Include(r => r.Race).ThenInclude(a => a.Animal)
               .Include(o => o.Owner);

            return await query.ToListAsync();
        }

        public async Task<Pet> UpdatePet(Pet pet)
        {
            _petContext.Update(pet);
            await _petContext.SaveChangesAsync();
            return pet;
        }
    }
}
