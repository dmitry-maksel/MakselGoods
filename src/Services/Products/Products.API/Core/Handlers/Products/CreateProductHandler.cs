using Products.API.Core.Queries.Products;

namespace Products.API.Core.Handlers.Products
{
    public class CreateProductHandler(ILogger<CreateProductHandler> logger, ProductsDbContext dbContext)
        : IRequestHandler<CreateProductQuery, int>
    {
        private readonly ProductsDbContext _dbContext = dbContext;
        private readonly ILogger<CreateProductHandler> _logger = logger;

        public async Task<int> Handle(CreateProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(CreateProductHandler), request);

            ArgumentNullException.ThrowIfNull(nameof(request));

            var product = new Product
            {
                Description = request.Description,
                Name = request.Name,
                CreatedAt = DateTimeOffset.UtcNow,
                ModifiedAt = DateTimeOffset.UtcNow
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
