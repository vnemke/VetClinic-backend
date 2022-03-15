using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class ControlRepository : IControlRepository
    {

        private readonly ApplicationDbContext _controlContext;

        public ControlRepository(ApplicationDbContext controlCotnext)
        {
            _controlContext = controlCotnext;
        }

        public async Task<Control> AddControl(Control control)
        {
            _controlContext.Set<Control>().Add(control);
            await _controlContext.SaveChangesAsync();
            return control;
        }

        public async Task<Control> DeleteControl(int controlId)
        {
            var entity = await _controlContext.Set<Control>().FindAsync(controlId);
            if (entity == null)
            {
                return entity;
            }

            _controlContext.Set<Control>().Remove(entity);
            await _controlContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Control>> GetAllControls()
        {
            IQueryable<Control> query = _controlContext.Set<Control>();

            return await query.ToListAsync();
        }

        public async Task<Control> GetControl(int controlId)
        {
            IQueryable<Control> query = _controlContext.Set<Control>()
                .Where(c => c.Id == controlId);
               

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Control>> GetControlsByCase(int caseId)
        {
            IQueryable<Control> query = _controlContext.Set<Control>()
                .Where(c => c.CaseId == caseId)
                .Include(c => c.Case);

            return await query.ToListAsync();
        }

        public async Task<List<Control>> GetControlsByDate()
        {
            IQueryable<Control> query = _controlContext.Set<Control>()
               .OrderByDescending(c => c.Date)
               .Include(c => c.Case);

            return await query.ToListAsync();
        }

        public async Task<Control> UpdateControl(Control control)
        {
            _controlContext.Update(control);
            await _controlContext.SaveChangesAsync();
            return control;
        }

       
    }
}
