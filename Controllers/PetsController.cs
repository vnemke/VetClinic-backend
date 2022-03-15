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
    public class PetsController : ControllerBase
    {
        private readonly IPetRepository _repository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public PetsController(IPetRepository repository, IOwnerRepository ownerRepository, IMapper mapper)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPetDto>>> GetAllPets()
        {
            try
            {
                var results = await _repository.GetAllPets();

                var mappedEntities = _mapper.Map<IEnumerable<GetPetDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{petId}")]
        public async Task<ActionResult<GetPetDto>> GetPet(int petId)
        {
            try
            {
                var result = await _repository.GetPet(petId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<GetPetDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("searchByDate")]
        public async Task<ActionResult<IEnumerable<GetPetDto>>> GetPetByDate()
        {
            try
            {
                var results = await _repository.GetPetsByDate();
                if (!results.Any()) return NotFound();

                var mappedEntities = _mapper.Map<IEnumerable<GetPetDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("searchByAnimal/{animalId}")]
        public async Task<ActionResult<IEnumerable<GetPetDto>>> GetPetsByAnimal(int animalId)
        {
            try
            {
                var results = await _repository.GetPetsByAnimal(animalId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [HttpGet("searchByOwner/{ownerId}")]
        public async Task<ActionResult<IEnumerable<GetPetDto>>> GetPetsByOwner(int ownerId)
        {
            try
            {
                var results = await _repository.GetPetsByOwner(ownerId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<WritePetDto>>> AddPet(WritePetDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Pet>(dto);
                await _repository.AddPet(mappedEntity);
                return Created($"/api/pets/{mappedEntity.Id}", _mapper.Map<WritePetDto>(mappedEntity));

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPost("{petWithOwner}")]
        public async Task<ActionResult<IEnumerable<PetWithOwnerDto>>> AddPetWithOwner([FromBody] PetWithOwnerDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<PetWithOwner>(dto);

                await _ownerRepository.AddOwner(mappedEntity.Owner);
                int id = mappedEntity.Owner.Id;

                mappedEntity.Pet.OwnerId = id;
                await _repository.AddPet(mappedEntity.Pet);

                return Ok(mappedEntity);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPut("{petId}")]
        public async Task<ActionResult<WritePetDto>> UpdatePet(int petId, WritePetDto dto)
        {
            try
            {
                var petEntity = await _repository.GetPet(petId);
                if (petEntity == null) return NotFound($"Could not find pet");

                var mappedPet = _mapper.Map(dto, petEntity);
                await _repository.UpdatePet(mappedPet);
                return NoContent();
            }
            catch (Exception)
            {
                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                throw;
            }

        }


        [HttpDelete("{petId}")]
        public async Task<ActionResult<Pet>> DeletePet(int petId)
        {
            try
            {
                var oldCase = await _repository.DeletePet(petId);
                if (oldCase == null) return NotFound($"Could not find pet");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
