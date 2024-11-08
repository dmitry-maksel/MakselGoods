using Microsoft.EntityFrameworkCore;
using Products.API.Core.Data;
using Products.API.Core.Queries.Products;

namespace Products.API.Core.Handlers.Products
{
    public class RemoveProductHandler(ILogger<RemoveProductHandler> logger, ProductsDbContext dbContext)
        : IRequestHandler<RemoveProductQuery, bool>
    {
        private readonly ProductsDbContext _dbContext = dbContext;
        private readonly ILogger<RemoveProductHandler> _logger = logger;

        public async Task<bool> Handle(RemoveProductQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(RemoveProductHandler), request);

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with id:{id} not found", request.Id);
                return false;
            }

            product.DeletedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
