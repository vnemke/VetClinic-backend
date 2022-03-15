using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Services
{
    public interface ICaseRepository
    {
        Task<List<Case>> GetAllCases();
        Task<Case> GetCase(int caseId);
        Task<List<Case>> GetCasesByPet(int petId);
        Task<List<VetCase>> GetCasesByVet(int vetId);
        Task<List<Case>> GetCasesByDate();
       
        Task<Case> AddCase(Case entity);
        Task<Case> UpdateCase(Case entity);
        Task<Case> DeleteCase(int caseId);
        //Task<IActionResult> PayAsync(PaymentModel model, int caseId);
    }
}
