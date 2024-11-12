using EventBus.Abstractions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Reviews.API.Core.Events;
using Reviews.API.Extensions;
using Reviews.API.Grpc;
using Reviews.API.Infrastructure.IntegrationEvents.EventHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.InstallServicesInAssembly(builder.Configuration);

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(opts =>
{
    if (builder.Configuration.GetValue<string>("IsDockerRun") == bool.TrueString)
    {
        opts.ListenAnyIP(80);
        opts.ListenAnyIP(81, o =>
        {
            o.Protocols = HttpProtocols.Http2;
        });
    }
});

var app = builder.Build();

await app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// TODO Enhance Event Subscribing
var eventSubscriber = app.Services.GetRequiredService<IEventSubscriber>();

var handler = app.Services.GetRequiredService<DisplayNameChangedEventHandler>();
eventSubscriber.Subscribe<DisplayNameChangedEvent>(handler.HandleEventAsync);

app.MapGrpcService<ReviewsService>();


app.Run();
