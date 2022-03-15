using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IAuthRepository
    {
        Task<bool> CheckUser(User entity);
        //Task<bool> CheckCredentials(string username, string password);
        Task<User> Register(User entity);
        Task<string> Login(string username, string password);
    }
}
