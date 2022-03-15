using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class XraysController : ControllerBase
    {
        private readonly IXrayRepository _repository;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _webHostEnvironment;

        public XraysController(IXrayRepository repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
           
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<XrayDto>>> GetAllXrays()
        {
            try
            {
                var results = await _repository.GetAllXrays();

                var mappedEntities = _mapper.Map<IEnumerable<XrayDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{xrayId}")]
        public async Task<ActionResult<XrayDto>> GetXray(int xrayId)
        {
            try
            {
                var result = await _repository.GetXray(xrayId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<XrayDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("searchByDate")]
        public async Task<ActionResult<IEnumerable<XrayDto>>> GetXraysByDate()
        {
            try
            {
                var results = await _repository.GetXraysByDate();
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("searchByCase/{caseId}")]
        public async Task<ActionResult<IEnumerable<XrayDto>>> GetXraysByCase(int caseId)
        {
            try
            {
                var results = await _repository.GetXraysByCase(caseId);
                if (!results.Any()) return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<XrayDto>>> AddXray(XrayDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Xray>(dto);
                await _repository.AddXray(mappedEntity);
                return Created($"/api/xrays/{mappedEntity.Id}", _mapper.Map<XrayDto>(mappedEntity));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<string> Upload([FromForm] XrayUploader obj)
        {
            if (obj.Files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Images\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Images\\");
                    }

                    await using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Images\\" + obj.Files.FileName))
                    {
                        obj.Files.CopyTo(fileStream);
                        fileStream.Flush();

                        string url = "\\Images\\" + obj.Files.FileName;
                        var result = JsonConvert.SerializeObject(url);

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                return "Upload is complete";
            }
            else
            {
                return "Upload Failed";
            }
        }


        //[HttpPost]
        //[Route("upload")]
        //public async Task<ActionResult<string>> Upload([FromForm] XrayUploader obj, XrayDto dto)
        //{
        //    if (obj.Files.Length > 0)
        //    {
        //        try
        //        {
        //            if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Images\\"))
        //            {
        //                Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Images\\");
        //            }

        //            await using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Images\\" + obj.Files.FileName))
        //            {
        //                obj.Files.CopyTo(fileStream);
        //                fileStream.Flush();
        //                var mappedEntity = _mapper.Map<Xray>(dto);
        //                mappedEntity.Url = "\\Images\\" + obj.Files.FileName;
        //                await _repository.AddXray(mappedEntity);
        //                return "\\Images\\" + obj.Files.FileName;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ex.ToString();
        //        }

        //        return "Upload is complete";
        //    }
        //    else
        //    {
        //        return "Upload Failed";
        //    }
        //}


        //[HttpPatch("{xrayId}")]
        //public async Task<ActionResult<XrayDto>> UpdateXray(int xrayId, [FromBody] JsonPatchDocument<XrayDto> patch)
        //{
        //    try
        //    {
        //        var oldXray = await _repository.GetXray(xrayId);
        //        if (oldXray == null) return NotFound($"Could not find xray");

        //        var dto = _mapper.Map<XrayDto>(oldXray);
        //        patch.ApplyTo(dto);

        //        var newXray = _mapper.Map(dto, oldXray);
        //        await _repository.UpdateXray(newXray);
        //        return Ok(newXray);
        //    }
        //    catch (Exception)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }
        //}

        [HttpDelete("{xrayId}")]
        public async Task<ActionResult<Xray>> DeleteXray(int xrayId)
        {
            try
            {
                var oldXray = await _repository.DeleteXray(xrayId);
                if (oldXray == null) return NotFound($"Could not find xray");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }
    }
}
