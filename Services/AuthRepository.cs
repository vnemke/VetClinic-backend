using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;
using RestSharp;
using RestSharp.Authenticators;
using FluentEmail.Mailgun;
using FluentEmail.Core;

namespace VetClinic.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _userContext;
        public readonly IConfiguration _configuration;

        RandomPasswordGenerator _randomPasswordGenerator = new RandomPasswordGenerator();
        MailService _mailService = new MailService();

        public AuthRepository(ApplicationDbContext userContext)
        {
            _userContext = userContext;  
        }


        public async Task<bool> CheckUser(User user)
        {
            IQueryable<User> query = _userContext.Set<User>().Where(u => u.Username == user.Username || u.Email == user.Email);
            var res = await query.FirstOrDefaultAsync();

            if (res == null)
            {
                return false;
            }
            return true;
        }

        public async Task<User> Register(User user)
        {
           
            user.Password = _randomPasswordGenerator.GeneratePassword(true, true, true, false);
            Console.WriteLine(user.Password);
            await _mailService.SendPass(user);

            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            user.PasswordSalt = mySalt;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, user.PasswordSalt);
            

            _userContext.Set<User>().Add(user);
            await _userContext.SaveChangesAsync();

            return user;
        }

        public async Task<string> Login(string username, string password)
        {
            IQueryable<User> query = _userContext.Set<User>().Where(u => u.Username == username).Include(r => r.Role);
            var user = await query.FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            var baseSalt = user.PasswordSalt;

            password = BCrypt.Net.BCrypt.HashPassword(password, baseSalt);

            if (user.Password == password)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_KEY")));
                var JWToken = new JwtSecurityToken(
                    claims: new List<Claim>
                    {
                        new Claim("userId", user.Id.ToString()),
                        new Claim("username", user.Username),
                        new Claim("fullName", user.FirstName+" "+user.LastName),
                        new Claim("role", user.Role.Name)
                    },
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);

                var result = JsonConvert.SerializeObject(token);

                return result;
            }

            return null;
        }

       
    }
}
