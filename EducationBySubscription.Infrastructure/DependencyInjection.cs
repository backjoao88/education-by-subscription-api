using EducationBySubscription.Application.Providers.Authentication;
using EducationBySubscription.Application.Providers.Payment;
using EducationBySubscription.Application.Providers.Payment.Serialization;
using EducationBySubscription.Application.Providers.Storage;
using EducationBySubscription.Application.Providers.Vault;
using EducationBySubscription.Infrastructure.Jobs;
using EducationBySubscription.Infrastructure.Persistence;
using EducationBySubscription.Infrastructure.Persistence.Configurations;
using EducationBySubscription.Infrastructure.Persistence.Interceptors;
using EducationBySubscription.Infrastructure.Persistence.Repositories;
using EducationBySubscription.Infrastructure.Providers.Asaas;
using EducationBySubscription.Infrastructure.Providers.Asaas.Clients;
using EducationBySubscription.Infrastructure.Providers.Asaas.Contracts;
using EducationBySubscription.Infrastructure.Providers.Asaas.Options;
using EducationBySubscription.Infrastructure.Providers.Asaas.Serialization;
using EducationBySubscription.Infrastructure.Providers.Authentication;
using EducationBySubscription.Infrastructure.Providers.Authentication.Options;
using EducationBySubscription.Infrastructure.Providers.Storage;
using EducationBySubscription.Infrastructure.Providers.Storage.Options;
using EducationBySubscription.Infrastructure.Providers.Vault;
using EducationBySubscription.Infrastructure.Providers.Vault.Options;
using EducationSubscription.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace EducationBySubscription.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .ConfigureOptions<AppDbContextOptionsSetup>()
            .AddDbContext<AppDbContext>(((provider, builder) =>
            {
                var appDbContextOptions =
                    provider.GetService(typeof(IOptions<AppDbContextOptions>)) as IOptions<AppDbContextOptions>;
                if (appDbContextOptions is null) return;
                builder.UseSqlServer(appDbContextOptions.Value.ConnectionString);
                builder.AddInterceptors(new ConvertEventsInterceptor());
            }))
            .AddScoped<IUnitOfWork, AppUnitOfWork>()
            .AddScoped<IMemberRepository, MemberRepository>()
            .AddScoped<ICourseRepository, CourseRepository>()
            .AddScoped<ISubscriptionRepository, SubscriptionRepository>()
            .AddScoped<IPlanRepository, PlanRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IPaymentRepository, PaymentRepository>();
        return services;
    }
    
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        services
            .ConfigureOptions<JwtOptionsSetup>()
            .ConfigureOptions<JwtBearerOptionsSetup>()
            .AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }


    public static IServiceCollection AddAzureStorage(this IServiceCollection services)
    {
        services
            .ConfigureOptions<AzureBlobStorageOptionsSetup>()
            .AddScoped<IStorageProvider, AzureBlobStorageProvider>();
        return services;
    }

    public static IServiceCollection AddAzureKeyVault(this IServiceCollection services)
    {
        services
            .ConfigureOptions<AzureKeyVaultOptionsSetup>()
            .AddScoped<IVaultProvider, AzureKeyVaultProvider>();
        return services;
    }
    
    public static IServiceCollection AddAsaas(this IServiceCollection services)
    {
        services
            .ConfigureOptions<PaymentProviderOptionsSetup>()
            .AddScoped<IPaymentProvider, AsaasProvider>()
            .AddScoped<IDefaultHttpSerializer, AsaasHttpSerializer>()
            .AddHttpClient<IDefaultHttpClient, AsaasHttpClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<PaymentProviderOptions>>().Value;
                client.BaseAddress = new Uri(AsaasResources.SandboxBaseEndpoint);
                client.DefaultRequestHeaders.Add("access_token", options.ApiKey);
            });
        return services;
    }
    
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services
            .AddQuartz(o =>
            {
                var jobKey = new JobKey("MediatorPublishMessagesJob");
                o.AddJob<MediatorPublishOutboxMessagesJob>(opts => opts.WithIdentity(jobKey));
                o.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .WithIdentity($"{jobKey.Name}Trigger")
                        .WithCronSchedule("0/10 * * ? * *")
                    );
            });
        services.AddQuartzHostedService(o => o.WaitForJobsToComplete = true);
        return services;
    }
    
    public static IServiceProvider RunMigrations(this IServiceProvider provider)
    {
        var scope = provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        return provider;
    }
}