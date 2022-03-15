using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class PetServiceRepository : IPetServiceRepository
    {
        private readonly ApplicationDbContext _petServiceContext;

        public PetServiceRepository(ApplicationDbContext petServiceContext)
        {
            _petServiceContext = petServiceContext;
        }

        public async Task<PetService> AddPetService(PetService entity)
        {
            _petServiceContext.Set<PetService>().Add(entity);
            await _petServiceContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PetService> DeletePetService(int petServiceId)
        {
            var entity = await _petServiceContext.Set<PetService>().FindAsync(petServiceId);
            if (entity == null)
            {
                return entity;
            }

            _petServiceContext.Set<PetService>().Remove(entity);
            await _petServiceContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PetService>> GetAllPetServices()
        {
            IQueryable<PetService> query = _petServiceContext.Set<PetService>()
                .Include(cp => cp.CasePetServices);

            return await query.ToListAsync();
        }

        public async Task<PetService> GetPetService(int petServiceId)
        {
            IQueryable<PetService> query =  _petServiceContext.Set<PetService>()
                .Where(p => p.Id == petServiceId)
                .Include(cp => cp.CasePetServices);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PetService> UpdatePetService(PetService petService)
        {
            _petServiceContext.Update(petService);
            await _petServiceContext.SaveChangesAsync();
            return petService;
        }
    }
}
