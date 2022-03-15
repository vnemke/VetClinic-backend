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
    public class ControlsController : ControllerBase
    {
        private readonly IControlRepository _repository;
        private readonly IMapper _mapper;

        public ControlsController(IControlRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlDto>>> GetAllControls()
        {
            try
            {
                var results = await _repository.GetAllControls();

                var mappedEntities = _mapper.Map<IEnumerable<ControlDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{controlId}")]
        public async Task<ActionResult<ControlDto>> GetControl(int controlId)
        {
            try
            {
                var result = await _repository.GetControl(controlId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<ControlDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

        }

        [HttpGet("searchByDate")]
        public async Task<ActionResult<IEnumerable<ControlDto>>> GetControlsByDate()
        {
            try
            {
                var results = await _repository.GetControlsByDate();
                if (!results.Any()) return NotFound();

                var mappedEntities = _mapper.Map<IEnumerable<ControlDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("searchByCase/{caseId}")]
        public async Task<ActionResult<IEnumerable<ControlDto>>> GetControlsByCase(int caseId)
        {
            try
            {
                var results = await _repository.GetControlsByCase(caseId);
                if (!results.Any()) return NotFound();

                var mappedEntities = _mapper.Map<IEnumerable<ControlDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<ControlDto>>> AddControl(ControlDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Control>(dto);
                await _repository.AddControl(mappedEntity);
                return Created($"/api/cases/{mappedEntity.Id}", _mapper.Map<ControlDto>(mappedEntity));

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }


        [HttpPatch("{controlId}")]
        public async Task<ActionResult<ControlDto>> UpdateControl(int controlId, [FromBody] JsonPatchDocument<ControlDto> patch)
        {
            try
            {
                var oldControl = await _repository.GetControl(controlId);
                if (oldControl == null) return NotFound($"Could not find control");

                var dto = _mapper.Map<ControlDto>(oldControl);
                patch.ApplyTo(dto);

                var newControl = _mapper.Map(dto, oldControl);
                await _repository.UpdateControl(newControl);
                return Ok(newControl);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{controlId}")]
        public async Task<ActionResult<Control>> DeleteControl(int controlId)
        {
            try
            {
                var oldControl = await _repository.DeleteControl(controlId);
                if (oldControl == null) return NotFound($"Could not find control");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }

    }
}
