using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    public class VetsController : ControllerBase
    {
        private readonly IVetRepository _repository;
        private readonly IMapper _mapper;

        public VetsController(IVetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VetDto>>> GetAllVets()
        {
            try
            {
                var results = await _repository.GetAllVets();

                var mappedEntities = _mapper.Map<IEnumerable<VetDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{vetId}")]
        public async Task<ActionResult<VetDto>> GetVet(int vetId)
        {
            try
            {
                var result = await _repository.GetVet(vetId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<VetDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("searchByCase/{caseId}")]
        public async Task<ActionResult<IEnumerable<VetDto>>> GetVetsByCase(int caseId)
        {
            try
            {
                var results = await _repository.GetVetsByCase(caseId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<VetDto>>> AddVet(VetDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Vet>(dto);
                await _repository.AddVet(mappedEntity);
                return Created($"/api/vets/{mappedEntity.Id}", _mapper.Map<VetDto>(mappedEntity));

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPatch("{vetId}")]
        public async Task<ActionResult<VetDto>> UpdateVet(int vetId, [FromBody] JsonPatchDocument<VetDto> patch)
        {
            try
            {
                var oldVet = await _repository.GetVet(vetId);
                if (oldVet == null) return NotFound($"Could not find vet");

                var dto = _mapper.Map<VetDto>(oldVet);
                patch.ApplyTo(dto);

                var newVet = _mapper.Map(dto, oldVet);
                await _repository.UpdateVet(newVet);
                return Ok(newVet);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{vetId}")]
        public async Task<ActionResult<Vet>> DeleteVet(int vetId)
        {
            try
            {
                var oldVet = await _repository.DeleteVet(vetId);
                if (oldVet == null) return NotFound($"Could not find vet");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
