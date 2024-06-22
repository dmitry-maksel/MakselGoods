using Microsoft.EntityFrameworkCore;
using Products.API.Core.Data;
using Products.API.Core.Queries.Products;

namespace Products.API.Core.Handlers.Products
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<GetAllProductsHandler> _logger;


        public GetAllProductsHandler(ILogger<GetAllProductsHandler> logger, ProductsDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(GetAllProductsHandler), request);

            ArgumentNullException.ThrowIfNull(request);

            var products = await _dbContext.Products.ToListAsync(cancellationToken);

            return products;
        }
    }
}
