using Microsoft.EntityFrameworkCore;
using Products.API.Core.Queries.Products;

namespace Products.API.Core.Handlers.Products
{
    public class UpdateProductHandler(ILogger<UpdateProductHandler> logger, ProductsDbContext dbContext)
        : IRequestHandler<UpdateProductQuery, bool>
    {
        private readonly ProductsDbContext _dbContext = dbContext;
        private readonly ILogger<UpdateProductHandler> _logger = logger;

        public async Task<bool> Handle(UpdateProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(UpdateProductHandler), request);

            ArgumentNullException.ThrowIfNull(nameof(request));

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with id:{id} not found", request.Id);
                return false;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.ModifiedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
