using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface IControlRepository
    {
        Task<List<Control>> GetAllControls();
        Task<Control> GetControl(int caseId);
        Task<List<Control>> GetControlsByCase(int caseId);
        Task<List<Control>> GetControlsByDate();

        Task<Control> AddControl(Control entity);
        Task<Control> UpdateControl(Control entity);
        Task<Control> DeleteControl(int controlId);
    }
}
