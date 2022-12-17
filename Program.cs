
using BingoRoomApi;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using BingoRoomApi.Models;
using MongoDB.Driver.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsProduction())
{
    //var builtConfig = config.Build();
    var builtConfig = builder.Configuration;
    builder.Configuration.AddAzureKeyVault(new KeyVaultManagement(builtConfig).SecretClient, new KeyVaultSecretManager());
    var bingoRoomDatabaseSettings = new BingoRoomDatabaseSettings(builtConfig);
    bingoRoomDatabaseSettings.SetConnectionstring();
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BingoRoomService>();

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
