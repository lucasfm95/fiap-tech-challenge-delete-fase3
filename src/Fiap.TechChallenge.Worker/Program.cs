using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Application.Services;
using Fiap.TechChallenge.Application.Services.Interfaces;
using Fiap.TechChallenge.Delete.Fase3.Worker.Consumers;
using Fiap.TechChallenge.Infrastructure.Context;
using Fiap.TechChallenge.Infrastructure.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env != "IntegrationTests")
{
    builder.Services.AddHealthChecks()
        .AddNpgSql(Environment.GetEnvironmentVariable("CONNECTION_STRING_DB_POSTGRES") ?? 
                   throw new Exception("CONNECTION_STRING_DB_POSTGRES not found."));
}



builder.Services.AddMassTransit(x => 
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        // cfg.ReceiveEndpoint("contact-delete-queue", e =>
        // {
        //     e.ConfigureConsumer<ContactDeletedConsumer>(context);
        // });
        cfg.ConfigureEndpoints(context);
    });
    x.AddConsumer<ContactDeletedConsumer>();
});

builder.Services.AddDbContext<ContactDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING_DB_POSTGRES"));
});

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService,ContactService>();

var host = builder.Build();
host.Run();