using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Application.Extensions;
using Persistence.Repository.IRepositories.GestionIRepositories;
using Persistence.Repository.Repositories.GestionRepositories;
using Application.Services.GestionServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 30))));  // Ajusta la versión de MySQL si es diferente

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyeccion de dependencias (Módulos Admin)
builder.Services.AddInterfacesInjection();
builder.Services.AddServiceDependency();

// Inyección de dependencias - Módulo Gestión (Estudiante)
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();

var app = builder.Build();

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