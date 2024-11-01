﻿using Microsoft.EntityFrameworkCore;
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
            ArgumentNullException.ThrowIfNull(nameof(request));

            _logger.LogInformation("{handlerName} STARTED with request: {requst}", nameof(UpdateProductHandler), request);

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("{handlerName} Product with id:{id} not found", nameof(UpdateProductHandler), request.Id);
                return false;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.ModifiedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("{handlerName} FINISHED with request: {requst}", nameof(UpdateProductHandler), request);

            return true;
        }
    }
}
