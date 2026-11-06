using Azure.Monitor.OpenTelemetry.AspNetCore;
using HostLibrary.Metrics;
using HostLibrary;
using System.Diagnostics;
async Task<string> secondHop()
{
    using var activity = ApiTracing.Source.StartActivity("GetSomething");
    activity?.SetTag("forecast.hoptwo", false);
   
    if (Random.Shared.Next(0,101) < 50)
    {
        activity?.AddEvent(new ActivityEvent("Failed to get something"));
        return "Failed";
    }
    activity?.AddEvent(new ActivityEvent("Succeded to get something"));
    return "Success";
}



var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton<ApiMetrics>();

builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
{
    options.ConnectionString = builder.Configuration["AzureMonitor:ConnectionString"];
})
    .WithMetrics(metrics => metrics.AddMeter(ApiMetrics.MeterName))
    .WithTracing(tracing => tracing.AddSource(ApiTracing.SourceName));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (ApiMetrics metrics,HttpContext ctx) =>
{
    using var activity = ApiTracing.Source.StartActivity("GetForecast");
    metrics.ForecastsHit(ctx.Request.Method);
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    activity?.AddEvent(new ActivityEvent ("Checking something"));
    await secondHop();

    activity?.AddEvent(new ActivityEvent("Forecast sent"));
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

