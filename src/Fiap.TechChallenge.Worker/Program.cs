using Fiap.TechChallenge.Infrastructure.Context;
using Fiap.TechChallenge.Worker.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Services.AddDbContext<ContactDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING_DB_POSTGRES"));
});

builder.Services.RegisterApplicationServices();
builder.Services.RegisterRepositories();
builder.Services.RegisterMessageBroker();

var app = builder.Build();

app.UseMetricServer(settings => settings.EnableOpenMetrics = false);
app.UseHttpMetrics();


app.Run();