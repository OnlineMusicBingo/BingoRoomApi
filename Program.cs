
using BingoRoomApi;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using BingoRoomApi.Models;
using MongoDB.Driver.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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

//TODO fix logger error
builder.Services.AddSingleton<BingoRoomService>();
//builder.Logging.AddProvider(loggerFactory);


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
