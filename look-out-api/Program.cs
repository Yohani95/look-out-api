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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LookDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("api_lookoutContext") ?? throw new InvalidOperationException("Connection string 'api_lookoutContext' not found.");
    var serverVersion = ServerVersion.AutoDetect(connectionString); // Detectar automáticamente la versión del servidor
    options.UseMySql(connectionString, serverVersion);
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();

//builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
