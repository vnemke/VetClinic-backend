using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Dto;
using VetClinic.Models;
using VetClinic.Services;

namespace VetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : Controller
    {
        private readonly ICaseRepository _repository;
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _caseContext;
        public readonly IOptions<StripeOptions> options;
        private readonly IStripeClient client;


        public CasesController(ICaseRepository repository, IMapper mapper, ApplicationDbContext caseContext, IOptions<StripeOptions> options)
        {
            _repository = repository;
            _mapper = mapper;
            _caseContext = caseContext;
            this.options = options;
            this.client = new StripeClient(this.options.Value.SecretKey);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCaseDto>>> GetAllCases()
        {
            try
            {
                var results = await _repository.GetAllCases();

                var mappedEntities = _mapper.Map<IEnumerable<GetCaseDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

        }

        [HttpGet("{caseId}")]
        public async Task<ActionResult<Case>> GetCase(int caseId)
        {
            try
            {
                var result = await _repository.GetCase(caseId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<GetCaseDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

        }


        [HttpGet("searchByDate")]
        public async Task<ActionResult<IEnumerable<GetCaseDto>>> GetCasesByDate()
        {
            try
            {
                var results = await _repository.GetCasesByDate();
                if (!results.Any()) return NotFound();

                var mappedEntities = _mapper.Map<IEnumerable<GetCaseDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("searchByPet/{petId}")]
        public async Task<ActionResult<IEnumerable<GetCaseDto>>> GetCasesByPet(int petId)
        {
            try
            {
                var results = await _repository.GetCasesByPet(petId);
                if (!results.Any()) return NotFound();

                var mappedEntities = _mapper.Map<IEnumerable<GetCaseDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        } 
        
        [HttpGet("searchByVet/{vetId}")]
        public async Task<ActionResult<IEnumerable<GetCaseDto>>> GetCasesByVet(int vetId)
        {
            try
            {
                var results = await _repository.GetCasesByVet(vetId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<WriteCaseDto>>> AddCase(WriteCaseDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Case>(dto);
                await _repository.AddCase(mappedEntity);
                return Created($"/api/cases/{mappedEntity.Id}", _mapper.Map<WriteCaseDto>(mappedEntity));

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPut("{caseId}")]
        public async Task<ActionResult<WriteCaseDto>> UpdateCase(int caseId, WriteCaseDto dto)
        {
            try
            {
                var caseEntity = await _repository.GetCase(caseId);
                if (caseEntity == null) return NotFound($"Could not find case");

                var mappedCase = _mapper.Map(dto, caseEntity);
                await _repository.UpdateCase(mappedCase);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{caseId}")]
        public async Task<ActionResult<Case>> DeleteCase(int caseId)
        {
            try
            {
                var oldCase = await _repository.DeleteCase(caseId);
                if (oldCase == null) return NotFound($"Could not find case");
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpGet("config")]
        public ConfigResponse GetConfig()
        {
            // return json: publishableKey (.env)
            return new ConfigResponse
            {
                PublishableKey = this.options.Value.PublishableKey,
            };
        }

        [HttpGet("payment/{caseId}")]
        public async Task<IActionResult> CreatePaymentIntent(int caseId)
        {
            var entity = await _caseContext.Set<Case>().FindAsync(caseId);

            IQueryable<PetService> query = _caseContext.Set<CasePetService>()
                .Where(c => c.CaseId == caseId)
                .Select(p => p.PetService);

            int total = 0;
            await query.ForEachAsync(p =>
            {
                total += p.Price;
            });
            Console.WriteLine(total);

            var options = new PaymentIntentCreateOptions
            {
                Amount = total * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                Metadata = new Dictionary<string, string>
                {
                    {"Id", caseId.ToString()},
                }

            };

            var service = new PaymentIntentService(this.client);

            try
            {
                var paymentIntent = await service.CreateAsync(options);

                return Ok(new CreatePaymentIntentResponse
                {
                    ClientSecret = paymentIntent.ClientSecret
                });
            }
            catch (Stripe.StripeException ex)
            {
                return BadRequest(new {
                    Error = new 
                    {
                        Message = ex.Message,
                    }
                });
            }
            catch (Exception)
            {

                return BadRequest();
            }  
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    this.options.Value.WebhookSecret
                    );
             
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine($"Payment Succeeded {paymentIntent.Id} for ${paymentIntent.Amount}");
                    Console.WriteLine(paymentIntent.Metadata["Id"]);

                    string param = paymentIntent.Metadata["Id"];
                    int res = Int32.Parse(param);
                    var entity = await _caseContext.Set<Case>().FindAsync(res);
                    entity.isPaid = true;
                    await _caseContext.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }

            return Ok();
        }
    }
}


