using WeatherApi.OpenMeteo;
using WeatherApi.OpenMeteo.Interfaces;
using WeatherApi.Repositories;
using WeatherApi.Repositories.Interfaces;
using WeatherApi.Services;
using WeatherApi.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// TODO: create/register middleware for token auth

builder.Services.AddSingleton(Log.Logger);
builder.Services.AddSingleton(typeof(IConfiguration), builder.Configuration);
builder.Services.AddSingleton<IWeatherRepository, WeatherRepository>();
builder.Services.AddSingleton<IOpenMeteoClient, OpenMeteoClient>();

builder.Services.AddTransient<IWeatherService, WeatherService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// TODO: configure CORS

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();