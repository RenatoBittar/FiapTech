using Microsoft.EntityFrameworkCore;
using PosFiapTech1.Application.Interfaces;
using PosFiapTech1.Application.Services;
using PosFiapTech1.Domain.Interfaces;
using PosFiapTech1.Infrastructure.Data;
using PosFiapTech1.Infrastructure.Repositories;
using PosFiapTech1.Middleware;
using Prometheus;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContatosContext>(options =>
    options.UseSqlServer(connectionString));

// Adicionar dependências
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure o middleware do Prometheus
app.UseRouting(); // Certifique-se de ter esta linha se estiver usando o routing.

// Middleware para coletar a latência das requisições
app.UseMiddleware<RequestTimingMiddleware>();

// Criar métricas para uso de CPU e memória
var cpuUsage = Metrics.CreateGauge("system_cpu_usage", "Uso da CPU do sistema");
var memoryUsage = Metrics.CreateGauge("system_memory_usage", "Uso da memória do sistema em bytes");

var timer = new System.Timers.Timer(15000); // 15 segundos
timer.Elapsed += (sender, e) =>
{
    var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
    cpuUsage.Set(cpuCounter.NextValue());

    var totalMemory = Process.GetCurrentProcess().PrivateMemorySize64;
    memoryUsage.Set(totalMemory);
};
timer.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeia as métricas do Prometheus
app.MapMetrics(); // Este endpoint expõe métricas em /metrics

app.UseHttpsRedirection();
app.UseAuthorization();
//app.MapGet("/metrics", async context =>
//{
//    var httpRequestDuration = Metrics.CreateHistogram("http_request_duration_seconds", "Duração das requisições HTTP em segundos");
//    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

//    await context.Response.WriteAsync("Hello from the metrics endpoint!");

//    stopwatch.Stop();
//    httpRequestDuration.Observe(stopwatch.Elapsed.TotalSeconds); // Registra a duração da requisição
//});
app.MapControllers();
app.Run();
