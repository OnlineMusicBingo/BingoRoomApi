
using BingoRoomApi;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using BingoRoomApi.Models;
using MongoDB.Driver.Core.Configuration;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

if (builder.Environment.IsProduction())
{
    var builtConfig = builder.Configuration;

    builder.Configuration.AddAzureKeyVault(new KeyVaultManagement(builtConfig).SecretClient, new KeyVaultSecretManager());
    BingoRoomDatabaseSettings.SetConnectionstring(builtConfig.GetValue<string>("BingoRoomDBConnString"));
}

if (builder.Environment.IsDevelopment())
{
    BingoRoomDatabaseSettings.SetConnectionstring(builder.Configuration["MONGODB_CONNSTRING"]);
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// create a logger factory
var loggerFactory = LoggerFactory.Create(
    builder => builder
                // add console as logging target
                .AddConsole()
                // add debug output as logging target
                .AddDebug()
                // set minimum level to log
                .SetMinimumLevel(LogLevel.Debug)
);

var logger = loggerFactory.CreateLogger("BingoRoomCategory");
//TODO fix logger error
builder.Services.AddSingleton<BingoRoomService>(logger);

// Test logging
logger.LogTrace("Trace message");
logger.LogDebug("Debug message");
logger.LogInformation("Info message");
logger.LogWarning("Warning message");
logger.LogError("Error message");
logger.LogCritical("Critical message");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
