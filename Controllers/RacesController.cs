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
    public class RacesController : ControllerBase
    {
        private readonly IRaceRepository _repository;
        private readonly IMapper _mapper;

        public RacesController(IRaceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetAllRaces()
        {
            try
            {
                var results = await _repository.GetAllRaces();

                var mappedEntities = _mapper.Map<IEnumerable<RaceDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{raceId}")]
        public async Task<ActionResult<RaceDto>> GetRace(int raceId)
        {
            try
            {
                var result = await _repository.GetRace(raceId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<RaceDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("searchByAnimal/{animalId}")]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetRacesByAnimal(int animalId)
        {
            try
            {
                var results = await _repository.GetRacesByAnimal(animalId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpPost()]
        public async Task<ActionResult<IEnumerable<RaceDto>>> AddRace(RaceDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Race>(dto);
                await _repository.AddRace(mappedEntity);
                return Created($"/api/races/{mappedEntity.Id}", _mapper.Map<RaceDto>(mappedEntity));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPatch("{raceId}")]
        public async Task<ActionResult<RaceDto>> UpdateRace(int raceId, [FromBody] JsonPatchDocument<RaceDto> patch)
        {
            try
            {
                var oldRace = await _repository.GetRace(raceId);
                if (oldRace == null) return NotFound($"Could not find race");

                var dto = _mapper.Map<RaceDto>(oldRace);
                patch.ApplyTo(dto);

                var newRace = _mapper.Map(dto, oldRace);
                await _repository.UpdateRace(oldRace);
                return Ok(newRace);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{raceId}")]
        public async Task<ActionResult<Race>> DeleteRace(int raceId)
        {
            try
            {
                var oldRace = await _repository.DeleteRace(raceId);
                if (oldRace == null) return NotFound($"Could not find race");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }

    }
}
