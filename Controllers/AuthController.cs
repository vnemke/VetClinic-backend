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
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<IEnumerable<UserRegisterDto>>> Register(UserRegisterDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<User>(dto);

                var res = await _repository.CheckUser(mappedEntity);
                if (res)
                {
                    return Conflict();
                }


                await _repository.Register(mappedEntity);
                
                return Created($"/api/auth/{mappedEntity.Id}", _mapper.Map<UserRegisterDto>(mappedEntity));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<string>>> Login(UserLoginDto dto)
        {
            try
            {
                var result = await _repository.Login(dto.Username, dto.Password);
                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
