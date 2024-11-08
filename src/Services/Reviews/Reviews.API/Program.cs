using EventBus.Abstractions;
using Reviews.API.Extensions;
using Reviews.API.Infrastructure.IntegrationEvents.EventHandlers;
using Reviews.API.Infrastructure.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.InstallServicesInAssembly(builder.Configuration);

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(opts =>
{
    opts.ListenAnyIP(80);
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

var eventSubscriber = app.Services.GetRequiredService<IEventSubscriber>();

var handler = app.Services.GetRequiredService<DisplayNameChangedEventHandler>();
eventSubscriber.Subscribe<DisplayNameChangedEvent>(handler.HandleEventAsync);


app.Run();
