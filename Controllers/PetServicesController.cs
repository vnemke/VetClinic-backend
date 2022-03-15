using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Dto;
using VetClinic.Models;
using VetClinic.Services;

namespace VetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetServicesController : ControllerBase
    {
        private readonly IPetServiceRepository _repository;
        private readonly IMapper _mapper;

        public PetServicesController(IPetServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetServiceDto>>> GetAllPetServices()
        {
            try
            {
                var results = await _repository.GetAllPetServices();

                var mappedEntities = _mapper.Map<IEnumerable<PetServiceDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{petServiceId}")]
        public async Task<ActionResult<PetServiceDto>> GetPetService(int petServiceId)
        {
            try
            {
                var result = await _repository.GetPetService(petServiceId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<PetServiceDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<PetServiceDto>>> AddPet(PetServiceDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<PetService>(dto);
                await _repository.AddPetService(mappedEntity);
                return Created($"/api/petservices/{mappedEntity.Id}", _mapper.Map<PetServiceDto>(mappedEntity));

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPut("{petServiceId}")]
        public async Task<ActionResult<PetServiceDto>> UpdatePetService(int petServiceId, PetServiceDto dto)
        {
            try
            {
                var petServiceEntity = await _repository.GetPetService(petServiceId);
                if (petServiceEntity == null) return NotFound($"Could not find pet service");

                var mappedPetService = _mapper.Map(dto, petServiceEntity);
                await _repository.UpdatePetService(petServiceEntity);
                return NoContent();
            }
            catch (Exception)
            {
                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                throw;
            }
        }

        [HttpDelete("{petServiceId}")]
        public async Task<ActionResult<PetService>> DeletePetService(int petServiceId)
        {
            try
            {
                var oldPetService = await _repository.DeletePetService(petServiceId);
                if (oldPetService == null) return NotFound($"Could not find pet service");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
