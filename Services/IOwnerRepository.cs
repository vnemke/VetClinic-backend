using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetAllOwners();
        Task<Owner> GetOwner(int ownerId);
      
        Task<Owner> AddOwner(Owner entity);
        Task<Owner> UpdateOwner(Owner entity);
        Task<Owner> DeleteOwner(int ownerId);
    }
}
