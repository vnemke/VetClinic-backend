using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _ownerContext;

        public OwnerRepository(ApplicationDbContext ownerContext)
        {
            _ownerContext = ownerContext;
        }

        public async Task<Owner> AddOwner(Owner owner)
        {
            _ownerContext.Set<Owner>().Add(owner);
            await _ownerContext.SaveChangesAsync();
            return owner;
        }

        public async Task<Owner> DeleteOwner(int ownerId)
        {
            var entity = await _ownerContext.Set<Owner>().FindAsync(ownerId);
            if (entity == null)
            {
                return entity;
            }

            _ownerContext.Set<Owner>().Remove(entity);
            await _ownerContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Owner>> GetAllOwners()
        {
            IQueryable<Owner> query = _ownerContext.Set<Owner>()
                .Include(p => p.Pets);
            return await query.ToListAsync();
        }

        public async Task<Owner> GetOwner(int ownerId)
        {
            IQueryable<Owner> query = _ownerContext.Set<Owner>()
               .Where(o => o.Id == ownerId)
               .Include(p => p.Pets);
              
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            _ownerContext.Update(owner);
            await _ownerContext.SaveChangesAsync();
            return owner;
        }

    }
}
