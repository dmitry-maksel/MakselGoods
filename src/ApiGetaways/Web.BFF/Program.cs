using Web.BFF.Protos;
using Web.BFF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var reviewsApiUrl = Environment.GetEnvironmentVariable("REVIEWS_API_URL") 
                    ?? "http://localhost:5053";

var productsApiUrl = Environment.GetEnvironmentVariable("PRODUCTS_API_URL")
                    ?? "http://localhost:5053";

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

app.Run();
