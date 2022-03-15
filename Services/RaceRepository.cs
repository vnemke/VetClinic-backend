using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _raceContext;

        public RaceRepository(ApplicationDbContext raceContext)
        {
            _raceContext = raceContext;
        }

        public async Task<Race> AddRace(Race race)
        {
            _raceContext.Set<Race>().Add(race);
            await _raceContext.SaveChangesAsync();
            return race;
        }

        public async Task<Race> DeleteRace(int raceId)
        {
            var entity = await _raceContext.Set<Race>().FindAsync(raceId);
            if (entity == null)
            {
                return entity;
            }

            _raceContext.Set<Race>().Remove(entity);
            await _raceContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Race>> GetAllRaces()
        {
            IQueryable<Race> query = _raceContext.Set<Race>()
               .Include(a => a.Animal);
              
            return await query.ToListAsync();
        }

        public async Task<Race> GetRace(int raceId)
        {
            IQueryable<Race> query = _raceContext.Set<Race>()
               .Where(r => r.Id == raceId)
               .Include(a => a.Animal);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Race>> GetRacesByAnimal(int animalId)
        {
            IQueryable<Race> query = _raceContext.Set<Race>()
              .Where(r => r.AnimalId == animalId)
              .Include(a => a.Animal);

            return await query.ToListAsync();
        }

        public async Task<Race> UpdateRace(Race race)
        {
            _raceContext.Update(race);
            await _raceContext.SaveChangesAsync();
            return race;
        }
    }
}
