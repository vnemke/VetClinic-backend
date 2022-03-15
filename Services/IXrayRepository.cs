using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IXrayRepository
    {
        Task<List<Xray>> GetAllXrays();
        Task<Xray> GetXray(int xrayId);
        Task<List<Xray>> GetXraysByCase(int caseId);
        Task<List<Xray>> GetXraysByDate();

        Task<Xray> AddXray(Xray entity);
        Task<Xray> UpdateXray(Xray entity);
        Task<Xray> DeleteXray(int xrayId);
    }
}
