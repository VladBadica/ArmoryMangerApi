using ArmoryManagerApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace ArmoryManagerApi.Middlewares;

public class TokenValidatorMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

        var user = string.Empty;
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedJwt = handler.ReadJwtToken(authorizationHeader.Replace("Bearer ", ""));

            user = decodedJwt.Claims.First(c => c.Type == "nameid").Value;
        }

        context.Request.Headers.Add("UserId", user);
        await _next.Invoke(context);
    }
}
