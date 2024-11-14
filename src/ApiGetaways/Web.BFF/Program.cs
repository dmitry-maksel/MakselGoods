using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Web.BFF.Protos;
using Web.BFF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var reviewsApiUrl = builder.Configuration["REVIEWS_API_URL"]
                    ?? throw new NullReferenceException("REVIEWS_API_URL is not defined");

var productsApiUrl = builder.Configuration["PRODUCTS_API_URL"]
                    ?? throw new NullReferenceException("PRODUCTS_API_URL is not defined");

builder.Services.AddGrpcClient<GrpcReviews.GrpcReviewsClient>(o =>
{
    o.Address = new Uri(reviewsApiUrl);
});
builder.Services.AddGrpcClient<GrpcProducts.GrpcProductsClient>(o =>
{
    o.Address = new Uri(productsApiUrl);
});
builder.Services.AddScoped<IProductService, ProductService>();

builder.WebHost.ConfigureKestrel(opts =>
{
    opts.ListenAnyIP(80);
});

builder.Services.AddOcelot();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
