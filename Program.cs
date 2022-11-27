using ArmoryManagerApi.Data;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Middlewares;

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

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(_policy);
       // app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}