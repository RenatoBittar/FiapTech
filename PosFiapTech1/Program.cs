using Microsoft.EntityFrameworkCore;
using PosFiapTech1.Application.Interfaces;
using PosFiapTech1.Application.Services;
using PosFiapTech1.Domain.Interfaces;
using PosFiapTech1.Infrastructure.Data;
using PosFiapTech1.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContatosContext>(options =>
    options.UseSqlServer(connectionString));

// Adicionar depend�ncias
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

// Add services to the container.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
