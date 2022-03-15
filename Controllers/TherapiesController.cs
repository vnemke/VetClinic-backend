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
    public class TherapiesController : ControllerBase
    {
        private readonly ITherapyRepository _repository;
        private readonly IMapper _mapper;

        public TherapiesController(ITherapyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TherapyDto>>> GetAllTherapies()
        {
            try
            {
                var results = await _repository.GetAllTherapies();

                var mappedEntities = _mapper.Map<IEnumerable<TherapyDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{therapyId}")]
        public async Task<ActionResult<TherapyDto>> GetTherapy(int therapyId)
        {
            try
            {
                var result = await _repository.GetTherapy(therapyId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<TherapyDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("searchByDate")]
        public async Task<ActionResult<IEnumerable<TherapyDto>>> GetTherapiesByDate()
        {
            try
            {
                var results = await _repository.GetTherapiesByDate();
                if (!results.Any()) return NotFound();

                var mappedEntities = _mapper.Map<IEnumerable<TherapyDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("searchByCase/{caseId}")]
        public async Task<ActionResult<IEnumerable<TherapyDto>>> GetTherapiesByCase(int caseId)
        {
            try
            {
                var results = await _repository.GetTherapiesByCase(caseId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<WriteTherapyDto>>> AddTherapy(WriteTherapyDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Therapy>(dto);
                await _repository.AddTherapy(mappedEntity);
                return Created($"/api/therapies/{mappedEntity.Id}", _mapper.Map<WriteTherapyDto>(mappedEntity));

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        //[HttpPatch("{therapyId}")]
        //public async Task<ActionResult<WriteTherapyDto>> UpdateTherapy(int therapyId, [FromBody] JsonPatchDocument<WriteTherapyDto> patch)
        //{
        //    try
        //    {
        //        var oldTherapy = await _repository.GetTherapy(therapyId);
        //        if (oldTherapy == null) return NotFound($"Could not find therapy");

        //        var dto = _mapper.Map<WriteTherapyDto>(oldTherapy);
        //        patch.ApplyTo(dto);

        //        var newTherapy = _mapper.Map(dto, oldTherapy);
        //        await _repository.UpdateTherapy(oldTherapy);
        //        return Ok(newTherapy);
        //    }
        //    catch (Exception)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }
        //}

        [HttpDelete("{therapyId}")]
        public async Task<ActionResult<Therapy>> DeleteTherapy(int therapyId)
        {
            try
            {
                var oldTherapy = await _repository.DeleteTherapy(therapyId);
                if (oldTherapy == null) return NotFound($"Could not find therapy");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
