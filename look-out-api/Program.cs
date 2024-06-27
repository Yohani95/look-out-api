using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Configuration;
using look.domain.interfaces.unitOfWork;
using Serilog;
using look.domain.entities.Common;
using look.Infrastructure.middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<LookDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("api_lookoutContext") ?? throw new InvalidOperationException("Connection string 'api_lookoutContext' not found.");
    var serverVersion = ServerVersion.AutoDetect(connectionString); // Detectar automáticamente la versión del servidor
    options.UseMySql(connectionString, serverVersion);
});
Logger.InitializeLogger();

var logger = Logger.GetLogger();
Log.Logger = logger;
Log.Information("Iniciando Servicio de Backend .net 6 look-out");
builder.Services.AddControllers();
Log.Information("Cargando inyección de dependencias");
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registrar las configuraciones como servicio
builder.Services.Configure<SharePointConfig>(configuration.GetSection("SharePointConfig"));

Log.Information("Esperando Solicitudes.....");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ErrorLoggingMiddleware>();
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
