using Microsoft.EntityFrameworkCore;
using Products.API.Core.CQRS.Queries;
using Products.API.Core.Entities;
using Products.API.Data;

namespace Products.API.Core.CQRS.Queries.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<GetAllProductsHandler> _logger;

        public GetProductByIdHandler(ILogger<GetAllProductsHandler> logger, ProductsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(GetProductByIdHandler), request);

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return product;
        }
    }
}
