using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class CaseRepository : ICaseRepository
    {
        private readonly ApplicationDbContext _caseContext;
        public readonly IOptions<StripeOptions> options;
        private readonly IStripeClient client;
       
        public CaseRepository(ApplicationDbContext caseContext, IOptions<StripeOptions> options)
        {
            _caseContext = caseContext;
            this.options = options;
            this.client = new StripeClient(this.options.Value.SecretKey);
        }

        public async Task<Case> AddCase(Case entity)
        {
            _caseContext.Set<Case>().Add(entity);
            await _caseContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Case> DeleteCase(int caseId)
        {
            var entity = await _caseContext.Set<Case>().FindAsync(caseId);
            if (entity == null)
            {
                return entity;
            }

            _caseContext.Set<Case>().Remove(entity);
            await _caseContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Case>> GetCasesByPet(int PetId)
        {
            IQueryable<Case> query = _caseContext.Set<Case>()
                .Where(p => p.Pet.Id == PetId)
                .Include(p => p.Pet);
                
            return await query.ToListAsync();
        }

        public async Task<List<VetCase>> GetCasesByVet(int vetId)
        {
            IQueryable<VetCase> query = _caseContext.Set<VetCase>()
                .Where(cv => cv.VetId == vetId)
                .Include(c => c.Case)
                .Include(v => v.Vet);

            return await query.ToListAsync();
        }

        public async Task<List<Case>> GetAllCases()
        {
            IQueryable<Case> query = _caseContext.Set<Case>()
                .Include(p => p.Pet).ThenInclude(p => p.Race.Animal)
                .Include(t => t.Therapies)
                .Include(c => c.Controls)
                .Include(x => x.Xrays)
                .Include(vc => vc.VetCases).ThenInclude(v => v.Vet)
                .Include(cp => cp.CasePetServices).ThenInclude(p => p.PetService);

            return await query.ToListAsync();
        }

        public async Task<Case> UpdateCase(Case entity)
        {
            _caseContext.Update(entity);
            await _caseContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Case> GetCase(int caseId)
        {
            IQueryable<Case> query = _caseContext.Set<Case>()
                .Where(c => c.Id == caseId)
                .Include(p => p.Pet).ThenInclude(p => p.Race.Animal)
                .Include(p => p.Pet).ThenInclude(o => o.Owner)
                .Include(t => t.Therapies)
                .Include(c => c.Controls)
                .Include(x => x.Xrays)
                .Include(vc => vc.VetCases).ThenInclude(v => v.Vet)
                .Include(cp => cp.CasePetServices).ThenInclude(p => p.PetService);


            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Case>> GetCasesByDate()
        {
            IQueryable<Case> query = _caseContext.Set<Case>()
               .OrderByDescending(c => c.Date)
               .Include(p => p.Pet);

            return await query.ToListAsync();
        }

    }
}




//public async Task<IActionResult> PayAsync(PaymentModel model, int caseId)
//{
//    var entity = await _caseContext.Set<Case>().FindAsync(caseId);

//    IQueryable<PetService> query = _caseContext.Set<CasePetService>()
//        .Where(c => c.CaseId == caseId)
//        .Select(p => p.PetService);

//    int total = 0;
//    await query.ForEachAsync(p =>
//    {
//        total += p.Price;
//    });
//    Console.WriteLine(total);

//    var options = new PaymentIntentCreateOptions
//    {

//        Amount = total * 100,
//        Currency = model.Currency,
//        //Source = model.Source,
//        ReceiptEmail = model.ReceiptEmail
//    };
//    var service = new PaymentIntentService(this.client);
//    var paymentIntent = await service.CreateAsync(options);
//    return Ok(new { clientSecret = paymentIntent.ClientSecret });
//}




//public async Task<dynamic> PayAsync(PaymentModel model, int caseId)
//{
//    var entity = await _caseContext.Set<Case>().FindAsync(caseId);

//    IQueryable<PetService> query = _caseContext.Set<CasePetService>()
//        .Where(c => c.CaseId == caseId)
//        .Select(p => p.PetService);

//    int total = 0;
//    await query.ForEachAsync(p =>
//    {
//        total += p.Price;
//    });
//    Console.WriteLine(total);

//    var optionsToken = new TokenCreateOptions
//    {
//        Card = new TokenCardOptions
//        {
//            Number = model.CardNumber,
//            ExpMonth = model.Month,
//            ExpYear = model.Year,
//            Cvc = model.Cvc
//        }
//    };
//    var serviceToken = new TokenService();
//    Token stripeToken = await serviceToken.CreateAsync(optionsToken);

//    var options = new ChargeCreateOptions
//    {
//        Amount = total * 100,
//        Currency = model.Currency,
//        Description = "test",
//        Source = model.Source,
//        ReceiptEmail = model.ReceiptEmail
//    };

//    var options = new PaymentIntentCreateOptions
//    {

//        Amount = total * 100,
//        Currency = model.Currency,
//        Description = "test",
//        //Source = model.Source,
//        ReceiptEmail = model.ReceiptEmail
//    };

//    var service = new ChargeService();
//    Charge charge = await service.CreateAsync(options);

//    var service = new PaymentIntentService();

//    service.Create(options);

//    if (charge.Paid)
//    {
//        entity.isPaid = true;
//        _caseContext.Update(entity);
//        await _caseContext.SaveChangesAsync();
//        return entity;
//    }
//    else
//    {
//        return "failed";
//    }
//}