using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace VetClinic.Services
{
    public class PDFServiceRepository : IPDFServiceRepository
    {
        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationDbContext _caseContext;

        public PDFServiceRepository(IHttpClientFactory httpClientFactory, ApplicationDbContext caseContext)
        {
            _httpClientFactory = httpClientFactory;
            _caseContext = caseContext;
        }

        public async Task<Boolean> AddPDFCase(int caseId)
        {
            try
            {
                IQueryable<Case> query = _caseContext.Set<Case>()
               .Where(c => c.Id == caseId)
               .Include(p => p.Pet)
               .Include(p => p.Pet).ThenInclude(o => o.Owner)
               .Include(vc => vc.VetCases).ThenInclude(v => v.Vet)
               .Include(cp => cp.CasePetServices).ThenInclude(p => p.PetService);

                var options = new JsonSerializerOptions()
                {
                    MaxDepth = 0,
                    //IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true
                };
                var body = await query.FirstOrDefaultAsync();

                string json = JsonConvert.SerializeObject(body, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = _httpClientFactory.CreateClient("pdfService");
                var response = await client.PostAsync("/api/pdf", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return true;
            }

        }
    }
}
