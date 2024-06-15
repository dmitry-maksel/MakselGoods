using Identity.API.Extensions;
using Identity.API.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.InstallServicesInAssembly(configuration);

builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddControllers();


var app = builder.Build();

await app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
