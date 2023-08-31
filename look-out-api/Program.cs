using look.Infrastructure.data;
using System;

using Microsoft.EntityFrameworkCore;
using look.Infrastructure.repository.admin;
using look.domain.interfaces.admin;
using look.Application.interfaces.admin;
using look.Application.services.admin;
using System.Reflection;
using YourNamespace.Configuration;
using look.Application.interfaces;
using look.domain.interfaces;
using look.Application.services;
using look.domain.interfaces.unitOfWork;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LookDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("api_lookoutContext") ?? throw new InvalidOperationException("Connection string 'api_lookoutContext' not found.");
    var serverVersion = ServerVersion.AutoDetect(connectionString); // Detectar automáticamente la versión del servidor
    options.UseMySql(connectionString, serverVersion);
});
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Nivel mínimo de log
    .WriteTo.File("../logs/log-.txt", rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 7,
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}  {Message:lj}{NewLine}{Exception}"
    ) // Archivo de log
    .CreateLogger();
// Add services to the container.
Log.Information("Iniciando Servicio de Backend . net 6 look-out");
builder.Services.AddControllers();
Log.Information("Cargando inyección de dependencias");
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
Log.Information("Esperando Solicitudes.....");

//builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "look-out-api",
        Version = "v1"
    });
});

var app = builder.Build();
var allowedOrigins = app.Configuration.GetSection("AllowedOrigins").Get<string[]>();
app.UseCors(builder =>
{
    builder.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
