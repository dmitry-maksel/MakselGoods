using Reviews.API.Extensions;

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

app.Run();
