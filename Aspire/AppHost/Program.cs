var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sqlPassword = builder.AddParameter("sql-password", secret: true);
var postgresServer = builder
    .AddPostgres("postgres", password: sqlPassword)
    .WithEnvironment("POSTGRES_DB", "telegrambot")
    .WithDataVolume("tg-postgres");

var database = postgresServer.AddDatabase("tg");

var apiService = builder.AddProject<Projects.ApiService>("apiservice")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
