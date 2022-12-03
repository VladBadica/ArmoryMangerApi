using ArmoryManagerApi.Data;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Middlewares;
using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ArmoryManagerApi;

public class Program
{
    public static void Main(string[] args)
    {
        var _policy = "CorsPolicy";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: _policy,
                policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        builder.Services.AddDbContext<ArmoryManagerContext>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key
                };
            });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<TokenValidatorMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(_policy);
        // app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}