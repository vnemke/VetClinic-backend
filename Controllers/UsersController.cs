using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using VetClinic.Dto;
using VetClinic.Models;
using VetClinic.Services;

namespace VetClinic.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
        {
            try
            {
                var results = await _repository.GetAllUsers();

                var mappedEntities = _mapper.Map<IEnumerable<GetUserDto>>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetUserDto>> GetUser(int userId)
        {
            try
            {
                var result = await _repository.GetUser(userId);
                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<GetUserDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }


        [HttpPut("{userId}")]
        public async Task<ActionResult<WriteUserDto>> UpdateUser(int userId, WriteUserDto dto)
        {
            try
            {
                var userEntity = await _repository.GetUser(userId);
                if (userEntity == null) return NotFound($"Could not find user");

                var mappedUser = _mapper.Map(dto, userEntity);
                await _repository.UpdateUser(mappedUser);
                return NoContent();
            }
            catch (Exception)
            {
                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                throw;
            }
            
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<User>> DeleteUser(int userId)
        {
            try
            {
                var oldUser = await _repository.DeleteUser(userId);
                if (oldUser == null) return NotFound($"Could not find user");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return NoContent();
        }
    }
}
