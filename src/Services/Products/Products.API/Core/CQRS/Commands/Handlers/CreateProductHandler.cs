using Products.API.Core.Entities;
using Products.API.Data;

namespace Products.API.Core.CQRS.Commands.Handlers
{
    public class CreateProductHandler(ILogger<CreateProductHandler> logger, ProductsDbContext dbContext)
        : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ProductsDbContext _dbContext = dbContext;
        private readonly ILogger<CreateProductHandler> _logger = logger;

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(CreateProductHandler), request);

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
