using TelegramBot.ApiService.Infrastructure;
using TelegramBot.ApiService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Инициализируем БД, только если запуск из Docker`a. 
    // Это является исправлением проблемы с тем, что во время генерации документации nswag невозможно подключиться к БД
    _ = bool.TryParse(Environment.GetEnvironmentVariable("DOCKER_STATUS"), out var runningInDocker);
    if (runningInDocker)
    {
        await app.InitialiseDatabaseAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));
app.MapDefaultEndpoints();
app.MapEndpoints();

app.Run();
