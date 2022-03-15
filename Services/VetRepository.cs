using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class VetRepository : IVetRepository
    {
        private readonly ApplicationDbContext _vetContext;

        public VetRepository(ApplicationDbContext vetContext)
        {
            _vetContext = vetContext;
        }

        public async Task<Vet> AddVet(Vet vet)
        {
            _vetContext.Set<Vet>().Add(vet);
            await _vetContext.SaveChangesAsync();
            return vet;
        }

        public async Task<Vet> DeleteVet(int vetId)
        {
            var entity = await _vetContext.Set<Vet>().FindAsync(vetId);
            if (entity == null)
            {
                return entity;
            }

            _vetContext.Set<Vet>().Remove(entity);
            await _vetContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Vet>> GetAllVets()
        {
            IQueryable<Vet> query = _vetContext.Set<Vet>()
                .Include(vc => vc.VetCases);
                //.ThenInclude(c => c.Case);

            return await query.ToListAsync();
        }

        public async Task<Vet> GetVet(int vetId)
        {
            IQueryable<Vet> query = _vetContext.Set<Vet>()
              .Where(v => v.Id == vetId);
              //.Include(vc => vc.VetCases)
              //.ThenInclude(c => c.Case);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<VetCase>> GetVetsByCase(int caseId)
        {
            IQueryable<VetCase> query = _vetContext.Set<VetCase>()
                .Where(vc => vc.CaseId == caseId)
                .Include(c => c.Case)
                .Include(v => v.Vet);

            return await query.ToListAsync();
        }

        public async Task<Vet> UpdateVet(Vet vet)
        {
            _vetContext.Update(vet);
            await _vetContext.SaveChangesAsync();
            return vet;
        }
    }
}
