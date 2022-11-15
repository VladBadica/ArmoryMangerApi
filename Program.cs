using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Server.IIS;

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
        builder.Services.AddDbContext<ArmoryManagerContext>();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(_policy);
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}