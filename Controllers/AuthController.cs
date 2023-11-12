using ArmoryManagerApi.Data.Repositories;
using ArmoryManagerApi.DataTransferObjects.UserDtos;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

	public AuthController(ArmoryManagerContext context, IConfiguration configuration)
	{
        _context = context;
        _configuration = configuration;
        _userRepository = new UserRepository(context);
    }

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginReqDto login)
	{
		var user = await _userRepository.Authenticate(login.UserName, login.Password);
        
        if(user == null)
        {
            return Unauthorized();
        }

        var loginRes = new LoginResDto()
        {
            UserName = user.UserName,
            Token = CreateJWT(user)
        };

        return Ok(loginRes);
	}

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginReqDto login)
    {
        if (await _userRepository.UserAlreadyExists(login.UserName))
        {
            return BadRequest("User already exists");
        }

        _userRepository.Register(login.UserName, login.Password);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    private string CreateJWT(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value!));
        var claims = new Claim[] {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(365),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}
