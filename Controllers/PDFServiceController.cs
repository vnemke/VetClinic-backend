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
    public class PDFServiceController : ControllerBase
    {
        private readonly IPDFServiceRepository _repository;
        private readonly IMapper _mapper;

        public PDFServiceController(IPDFServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost("{caseId}")]
        public async Task<ActionResult<IEnumerable<Case>>> AddPDFCase(int caseId)
        {
            try
            {
                var response = await _repository.AddPDFCase(caseId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }
    }
}
