using ArmoryManagerApi.ViewModels;
using ArmoryManagerApi.Models;
using ArmoryManagerApi.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IConfiguration _configuration;

	public AuthController(ArmoryManagerContext context, IConfiguration configuration)
	{
        _context = context;
        _configuration = configuration;
    }

	[HttpPost("login")]
	public ActionResult<LoginResVM> Login(LoginReqVM login)
	{
        var user = _context.Users.FirstOrDefault(u => u.UserName == login.UserName);

        if (user == null || user.Salt == null)
        {
            return Unauthorized();
        }

        var hmac = new HMACSHA512(user.Salt);
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

        if (!user.Password.SequenceEqual(passwordHash))
        {
            return Unauthorized();
        }

        var loginRes = new LoginResVM()
        {
            UserName = user.UserName,
            Token = CreateJWT(user)
        };

        return Ok(loginRes);
	}

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginReqVM login)
    {
        if (_context.Users.Any(x => x.UserName == login.UserName))
        {
            return BadRequest("User already exists");
        }

        byte[] passwordHash, salt;

        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
        var user = new User()
        {
            UserName = login.UserName,
            Password = passwordHash,
            Salt = salt,
            CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT),
            UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)
        };

        _context.Users.Add(user);
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
