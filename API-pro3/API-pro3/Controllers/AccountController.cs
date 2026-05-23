using API_pro3.Dtos.UserDtos;
using API_pro3.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_pro3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(
        IValidator<RegisterDto> registerValidator,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config,
        IMapper mapper
        ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validationResult = registerValidator.Validate(registerDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var user = await userManager.FindByNameAsync(registerDto.UserName);
            if (user is not null) return BadRequest("Username already exists");
            user = mapper.Map<AppUser>(registerDto);
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            //todo : add role to user
            userManager.AddToRoleAsync(user, "Member");

            return Ok("User Registered Succesfully");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if(user is null) return BadRequest("Invalid username or password");
            var request = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!request) return BadRequest("Invalid username or password");
            //todo : generate token
            //claims
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Fullaname", user.FullName),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            //generate token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(new
            {
                token
            });
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    return Ok();
        //}
    }
}
