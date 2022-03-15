using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class XrayRepository : IXrayRepository
    {
        private readonly ApplicationDbContext _xrayContext;

        public XrayRepository(ApplicationDbContext xrayContext)
        {
            _xrayContext = xrayContext;
        }

        public async Task<Xray> AddXray(Xray xray)
        {
            _xrayContext.Set<Xray>().Add(xray);
            await _xrayContext.SaveChangesAsync();
            return xray;
        }

        public async Task<Xray> DeleteXray(int xrayId)
        {
            var entity = await _xrayContext.Set<Xray>().FindAsync(xrayId);
            if (entity == null)
            {
                return entity;
            }

            _xrayContext.Set<Xray>().Remove(entity);
            await _xrayContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Xray>> GetAllXrays()
        {
            IQueryable<Xray> query = _xrayContext.Set<Xray>()
               .Include(v => v.Case);

            return await query.ToListAsync();
        }

        public async Task<Xray> GetXray(int xrayId)
        {
            IQueryable<Xray> query = _xrayContext.Set<Xray>()
              .Where(x => x.Id == xrayId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Xray>> GetXraysByCase(int caseId)
        {
            IQueryable<Xray> query = _xrayContext.Set<Xray>()
              .Where(xc => xc.CaseId == caseId);

            return await query.ToListAsync();
        }

        public async Task<List<Xray>> GetXraysByDate()
        {
            IQueryable<Xray> query = _xrayContext.Set<Xray>()
               .OrderByDescending(p => p.Date)
               .Include(c => c.Case);

            return await query.ToListAsync();
        }

        public async Task<Xray> UpdateXray(Xray xray)
        {
            _xrayContext.Update(xray);
            await _xrayContext.SaveChangesAsync();
            return xray;
        }
    }
}
