using BingoRoomApi;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using BingoRoomApi.Services;
using BingoRoomApi.Data;
using BingoRoomApi.Interfaces;
using BingoRoomApi.Repositories;
using Microsoft.Extensions.Options;
using Azure.Core;
using Azure.Identity;
using System;
using MongoDB.Driver.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

if (builder.Environment.IsProduction())
{
    var builtConfig = builder.Configuration;

    Console.Write("AddAzureKeyvault:");
    builder.Configuration.AddAzureKeyVault(new KeyVaultManagement(builtConfig).SecretClient, new KeyVaultSecretManager())

    try
    {
        Console.Write("GetValue ConnString:");

        builtConfig.GetValue<string>("BingoRoomDBConnString");

        builder.Services.Configure<BingoAppDbSettings>(options => new BingoAppDbSettings
        {
            ConnectionString = builtConfig.GetValue<string>("BingoRoomDBConnString"),
            DatabaseName = builder.Configuration["bingo_app"]
        });
    }
    catch (Exception ex)
    {
        Console.Write("Error BingoRoomDBConnString");
        Console.Write(ex);

        builder.Services.Configure<BingoAppDbSettings>(builder.Configuration.GetSection("BingoAppDbSettings"));
    }
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<BingoAppDbSettings>(builder.Configuration.GetSection("BingoAppDbSettings"));
}


builder.Services.AddSingleton<IBingoAppDbSettings>(serviceProvider =>
        serviceProvider.GetRequiredService<IOptions<BingoAppDbSettings>>().Value);
builder.Services.AddSingleton<IBingoRoomRepository, BingoRoomRepository>();
builder.Services.AddSingleton<IBingoRoomService, BingoRoomService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
