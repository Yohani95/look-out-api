using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Configuration;
using look.domain.interfaces.unitOfWork;
using Serilog;
using look.domain.entities.Common;
using look.Infrastructure.middleware;
using look.domain.entities.admin;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<LookDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("api_lookoutContext") ?? throw new InvalidOperationException("Connection string 'api_lookoutContext' not found.");
    var serverVersion = ServerVersion.AutoDetect(connectionString); // Detectar autom�ticamente la versi�n del servidor
    options.UseMySql(connectionString, serverVersion);
});
Logger.InitializeLogger();

var logger = Logger.GetLogger();
Log.Logger = logger;
Log.Information("Iniciando Servicio de Backend .net 6 look-out");
builder.Services.AddControllers();
Log.Information("Cargando servicio de Emails");
// Registrar EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

Log.Information("Cargando inyecci�n de dependencias");
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registrar las configuraciones como servicio
builder.Services.Configure<SharePointConfig>(configuration.GetSection("SharePointConfig"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Aplicar migraciones al iniciar la aplicaci�n

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
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<LookDbContext>();

        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                //Log.Information("Aplicando migraciones....");
                //dbContext.Database.Migrate(); // Aplica las migraciones pendientes

                //// Confirma la transacci�n si todo es exitoso
                //transaction.Commit();
                //Log.Information("Migraciones aplicadas exitosamente.");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, realiza un rollback
                transaction.Rollback();
                Log.Error(ex, "Error aplicando migraciones, se realiz� rollback.");
                throw; // Propaga la excepci�n para manejarla seg�n sea necesario
            }
        }
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}
Log.Information("Esperando Solicitudes.....");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
