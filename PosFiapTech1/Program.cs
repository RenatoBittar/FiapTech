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
// Configure o middleware do Prometheus
app.UseRouting(); // Certifique-se de ter esta linha se estiver usando o routing.

// Middleware para coletar a lat�ncia das requisi��es
app.UseMiddleware<RequestTimingMiddleware>();

// Criar m�tricas para uso de CPU e mem�ria
var cpuUsage = Metrics.CreateGauge("system_cpu_usage", "Uso da CPU do sistema");
var memoryUsage = Metrics.CreateGauge("system_memory_usage", "Uso da mem�ria do sistema em bytes");

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

// Mapeia as m�tricas do Prometheus
app.MapMetrics(); // Este endpoint exp�e m�tricas em /metrics

app.UseHttpsRedirection();
app.UseAuthorization();
//app.MapGet("/metrics", async context =>
//{
//    var httpRequestDuration = Metrics.CreateHistogram("http_request_duration_seconds", "Dura��o das requisi��es HTTP em segundos");
//    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

//    await context.Response.WriteAsync("Hello from the metrics endpoint!");

//    stopwatch.Stop();
//    httpRequestDuration.Observe(stopwatch.Elapsed.TotalSeconds); // Registra a dura��o da requisi��o
//});
app.MapControllers();
app.Run();
