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
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _repository;
        private readonly IMapper _mapper;

        public AnimalsController(IAnimalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAllAnimals(/*[FromQuery(Name = "Name")] string name*/)
        {
            try
            {
                var results = await _repository.GetAllAnimals(/*name*/);
                var mappedEntities = _mapper.Map<IEnumerable<AnimalDto>>(results); 
                return Ok(mappedEntities);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            
        } 
        
        [HttpGet("{animalId}")]
        public async Task<ActionResult<AnimalDto>> GetAnimal(int animalId)
        {
            try
            {
                var result = await _repository.GetAnimal(animalId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<AnimalDto>(result); 
                return Ok(mappedEntity);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> AddAnimal(AnimalDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Animal>(dto);
                await _repository.AddAnimal(mappedEntity);
                return Created($"/api/animals/{mappedEntity.Id}", _mapper.Map<AnimalDto>(mappedEntity));

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPatch("{animalId}")]
        public async Task<ActionResult<AnimalDto>> UpdateAnimal(int animalId, [FromBody] JsonPatchDocument<AnimalDto> patch)
        {
            try
            {
                var oldAnimal = await _repository.GetAnimal(animalId);
                if (oldAnimal == null) return NotFound($"Could not find animal");

                var dto = _mapper.Map<AnimalDto>(oldAnimal);
                patch.ApplyTo(dto);

                var newAnimal = _mapper.Map(dto, oldAnimal);
                await _repository.UpdateAnimal(newAnimal);
                return Ok(newAnimal);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

       
        [HttpDelete("{animalId}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int AnimalId)
        {
            try
            { 
                var oldAnimal = await _repository.DeleteAnimal(AnimalId);
                if(oldAnimal == null) return NotFound($"Could not find animal");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
