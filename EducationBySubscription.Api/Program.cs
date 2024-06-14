using EducationBySubscription.Api.Middlewares;
using EducationBySubscription.Application;
using EducationBySubscription.Infrastructure;
using Serilog;

namespace EducationBySubscription.Api;

/// <summary>
/// Main application entrypoint.
/// </summary>
public abstract class App
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });
        builder.Services.AddScoped<ExceptionHandler>();
        builder.Services.AddControllers();
        builder.Services.AddAzureKeyVault();
        builder.Services.AddPersistence();
        builder.Services.AddJwt();
        builder.Services.AddMediator();
        builder.Services.AddBackgroundJobs();
        builder.Services.AddAsaas();
        builder.Services.AddAzureStorage();
        builder.Services.AddAuthentication().AddJwtBearer();
        builder.Services.AddAuthorization();
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandler>();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        app.Services.RunMigrations();
        app.Run();
    }
}