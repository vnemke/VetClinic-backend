using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface ITherapyRepository
    {
        Task<List<Therapy>> GetAllTherapies();
        Task<Therapy> GetTherapy(int therapyId);
        Task<List<Therapy>> GetTherapiesByCase(int caselId);
        Task<List<Therapy>> GetTherapiesByDate();

        Task<Therapy> AddTherapy(Therapy entity);
        Task<Therapy> UpdateTherapy(Therapy entity);
        Task<Therapy> DeleteTherapy(int therapyId);
    }
}
