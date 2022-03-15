using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task<User> UpdateUser(User entity);
        Task<User> DeleteUser(int userId);
    }
}
