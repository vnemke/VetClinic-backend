using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;


namespace VetClinic.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _userContext;

        public UserRepository(ApplicationDbContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var entity = await _userContext.Set<User>().FindAsync(userId);
            if (entity == null)
            {
                return entity;
            }

            _userContext.Set<User>().Remove(entity);
            await _userContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<User>> GetAllUsers()
        {
            IQueryable<User> query = _userContext.Set<User>()
                .Include(r => r.Role);

            return await query.ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            IQueryable<User> query = _userContext.Set<User>()
               .Where(u => u.Id == userId)
               .Include(r => r.Role);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            _userContext.Update(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

    }
}
