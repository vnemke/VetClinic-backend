using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class TherapyRepository : ITherapyRepository
    {
        private readonly ApplicationDbContext _therapyContext;

        public TherapyRepository(ApplicationDbContext therapyContext)
        {
            _therapyContext = therapyContext;
        }
        public async Task<Therapy> AddTherapy(Therapy therapy)
        {
            _therapyContext.Set<Therapy>().Add(therapy);
            await _therapyContext.SaveChangesAsync();
            return therapy;
        }

        public async Task<Therapy> DeleteTherapy(int therapyId)
        {
            var entity = await _therapyContext.Set<Therapy>().FindAsync(therapyId);
            if (entity == null)
            {
                return entity;
            }

            _therapyContext.Set<Therapy>().Remove(entity);
            await _therapyContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Therapy>> GetAllTherapies()
        {
            IQueryable<Therapy> query = _therapyContext.Set<Therapy>()
               .Include(c => c.Case);
               
            return await query.ToListAsync();
        }

        public async Task<List<Therapy>> GetTherapiesByCase(int caselId)
        {
            IQueryable<Therapy> query = _therapyContext.Set<Therapy>()
              .Where(c => c.CaseId == caselId)
              .Include(c => c.Case);
             
            return await query.ToListAsync();
        }

        public async Task<List<Therapy>> GetTherapiesByDate()
        {
            IQueryable<Therapy> query = _therapyContext.Set<Therapy>()
               .OrderByDescending(p => p.Date)
               .Include(c => c.Case);

            return await query.ToListAsync();
        }

        public async Task<Therapy> GetTherapy(int therapyId)
        {
            IQueryable<Therapy> query = _therapyContext.Set<Therapy>()
              .Where(t => t.Id == therapyId)
              .Include(c => c.Case);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Therapy> UpdateTherapy(Therapy therapy)
        {
            _therapyContext.Update(therapy);
            await _therapyContext.SaveChangesAsync();
            return therapy;
        }
    }
}
