namespace PosFiapTech1.Middleware;
using Microsoft.AspNetCore.Http;
using Prometheus;
using System.Diagnostics;
using System.Threading.Tasks;


public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(context); // Chama o próximo middleware

        stopwatch.Stop();
        // Registra a duração da requisição
        Metrics.CreateHistogram("http_request_duration_seconds", "Duração das requisições HTTP em segundos").Observe(stopwatch.Elapsed.TotalSeconds);
    }
}


