using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Infrastructure.Data;
using TelegramBot.ApiService.Infrastructure.Data.Interceptors;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__tg");
        
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString).AddAsyncSeeding(sp);
        });

        builder.EnrichNpgsqlDbContext<ApplicationDbContext>();
        
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ApplicationDbContextInitializer>();

        builder.Services.AddSingleton(TimeProvider.System);
    }
}
