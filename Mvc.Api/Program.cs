using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;
using Mvc.Bussnies.Persona;
using Mvc.Repository.PersonaRepo.Contratos;
using Mvc.Repository.PersonaRepo.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_demoContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inyección de dependencias - Repositories
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

// Inyección de dependencias - Business Logic
builder.Services.AddScoped<IPersonaBussnies, PersonaBussnies>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
