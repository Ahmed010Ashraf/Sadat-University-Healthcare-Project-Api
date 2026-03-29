using BLL.Dtos;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class AuthenticationService(UserManager<AppUser> _usermanager,IOptions<JwtOptions> options) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDto LoginDto)
        {
            var user = await _usermanager.FindByEmailAsync(LoginDto.Email) ?? throw new UnauthorizedException();
            var IsPassCorrect = await _usermanager.CheckPasswordAsync(user , LoginDto.Password);
            if (!IsPassCorrect)
            {
                throw new UnauthorizedException();
            }
            return new UserResultDto()
            {
                Name = user.FullName,
                Email = user.Email,
                Token = await CreateToken(user)
            };
        }

        public async Task<UserResultDto> Register(RegisterDto RegisterDto)
        {
            var user = new AppUser()
            {
                FullName = RegisterDto.FullName,
                Email = RegisterDto.Email,
                NationalId = RegisterDto.NationalId,
                UniversityRole = RegisterDto.UniversityRole,
                PhoneNumber = RegisterDto.PhoneNumber,
                UserName = RegisterDto.Email.Split("@")[0]
            };
            var res = await _usermanager.CreateAsync(user , RegisterDto.Password);
            if (!res.Succeeded) { 
                var errors = res.Errors.Select(e=>e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto()
            {
                Name = user.FullName,
                Email = user.Email,
                Token =await CreateToken(user)
            };

        }


        private async Task<string> CreateToken(AppUser User)
        {
            var JwtOptions = options.Value;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , User.FullName),
                new Claim (ClaimTypes.Email , User.Email),
                new Claim (ClaimTypes.NameIdentifier , User.Id.ToString())
            };

            var roles = await _usermanager.GetRolesAsync(User);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer:JwtOptions.Issuer
                ,audience:JwtOptions.Audience
                ,claims : claims 
                ,expires:DateTime.UtcNow.AddDays(JwtOptions.ExpirationInDays)
                ,signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
