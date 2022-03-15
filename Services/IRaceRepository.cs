using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IRaceRepository
    {
        Task<List<Race>> GetAllRaces();
        Task<Race> GetRace(int raceId);
        Task<List<Race>> GetRacesByAnimal(int animalId);
      
        Task<Race> AddRace(Race entity);
        Task<Race> UpdateRace(Race entity);
        Task<Race> DeleteRace(int raceId);
    }
}
