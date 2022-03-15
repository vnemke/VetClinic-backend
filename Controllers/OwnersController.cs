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
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _repository;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAllOwners()
        {
            try
            {
                var results = await _repository.GetAllOwners();

                var mappedEntities = _mapper.Map<IEnumerable<OwnerDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{ownerId}")]
        public async Task<ActionResult<OwnerDto>> GetControl(int ownerId)
        {
            try
            {
                var result = await _repository.GetOwner(ownerId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<OwnerDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> AddOwner(OwnerDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Owner>(dto);
                await _repository.AddOwner(mappedEntity);
                return Created($"/api/owners/{mappedEntity.Id}", _mapper.Map<OwnerDto>(mappedEntity));

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPatch("{ownerId}")]
        public async Task<ActionResult<OwnerDto>> UpdateOwner(int ownerId, [FromBody] JsonPatchDocument<OwnerDto> patch)
        {
            try
            {
                var oldOwner = await _repository.GetOwner(ownerId);
                if (oldOwner == null) return NotFound($"Could not find owner");

                var dto = _mapper.Map<OwnerDto>(oldOwner);
                patch.ApplyTo(dto);

                var newOwner = _mapper.Map(dto, oldOwner);
                await _repository.UpdateOwner(newOwner);
                return Ok(newOwner);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{ownerId}")]
        public async Task<ActionResult<Owner>> DeleteOwner(int ownerId)
        {
            try
            {
                var oldOwner = await _repository.DeleteOwner(ownerId);
                if (oldOwner == null) return NotFound($"Could not find owner");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
