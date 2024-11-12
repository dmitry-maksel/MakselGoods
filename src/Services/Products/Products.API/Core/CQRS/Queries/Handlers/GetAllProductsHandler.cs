using Microsoft.EntityFrameworkCore;
using Products.API.Core.CQRS.Queries;
using Products.API.Core.Entities;
using Products.API.Data;

namespace Products.API.Core.CQRS.Queries.Handlers
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
            ArgumentNullException.ThrowIfNull(request);

            _logger.LogInformation("{handlerName} STARTED with request: {requst}", nameof(GetAllProductsHandler), request);

            var products = await _dbContext.Products.ToListAsync(cancellationToken);

            _logger.LogInformation("{handlerName} FINISHED with request: {requst}", nameof(GetAllProductsHandler), request);

            return products;
        }
    }
}
