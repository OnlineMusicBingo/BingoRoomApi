using BingoRoomApi.Services;
using BingoRoomApi.Data;
using BingoRoomApi.Interfaces;
using BingoRoomApi.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

if (builder.Environment.IsProduction())
{
    var builtConfig = builder.Configuration;

    //Console.Write("AddAzureKeyvault:");
    //builder.Configuration.AddAzureKeyVault(new KeyVaultManagement(builtConfig).SecretClient, new KeyVaultSecretManager());
    builder.Services.Configure<BingoAppDbSettings>(builder.Configuration.GetSection("BingoAppDbSettings"));

    /*try
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
        Console.Write(ex
    }*/
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

builder.Services.AddSingleton<RpcServer>();

// Build the service provider
var serviceProvider = builder.Services.BuildServiceProvider();

// Get the service from the service provider
var rpcServer = serviceProvider.GetRequiredService<RpcServer>();

// Start RpcServer
rpcServer.Start();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
