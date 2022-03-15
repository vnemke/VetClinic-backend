using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _roleContext;

        public RoleRepository(ApplicationDbContext roleContext)
        {
            _roleContext = roleContext;
        }


        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleContext.Set<Role>().ToListAsync();
        }

        public async Task<Role> GetRole(int roleId)
        {
            IQueryable<Role> query = _roleContext.Set<Role>()
                .Where(r => r.Id == roleId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
