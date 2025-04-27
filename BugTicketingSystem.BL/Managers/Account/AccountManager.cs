using BugTicketingSystem.BL.DTOs.Account;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Account
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;

        private static readonly List<string> allowedRoles = new()
        {
        "Admin", "Manager", "Developer", "Tester"
        };
        

        public AccountManager(UserManager<Users> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<string> Register(RegisterDto register)
        {
            if(!allowedRoles.Contains(register.Role))
            {
                return "Role is not allowed.";
            }

            if(!await _roleManager.RoleExistsAsync(register.Role))
            {
                return "Role does not exist in the database.";
            }
            var user = new Users
            {
                UserName = register.UserName,
                Email = register.Email,
                FullName = register.FullName
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, register.Role);
                return "User registered and assigned role successfully.";
            }
            else
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }
        }

        public async Task<LoginResultDto?> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResultDto
            {
                Message = "Login successful ",
                Token = tokenString
            };
        }



    }
}
