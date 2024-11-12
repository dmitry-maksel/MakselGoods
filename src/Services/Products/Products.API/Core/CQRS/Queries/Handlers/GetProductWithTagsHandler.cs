using Microsoft.EntityFrameworkCore;
using Products.API.Core.CQRS.Commands.Handlers;
using Products.API.Core.CQRS.Queries;
using Products.API.Core.Models;
using Products.API.Data;

namespace Products.API.Core.CQRS.Queries.Handlers
{
    public class GetProductWithTagsHandler : IRequestHandler<GetProductWithTagsQuery, ProductWithTagsDto>
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<AddTagsToProductHandler> _logger;

        public GetProductWithTagsHandler(ProductsDbContext dbContext, ILogger<AddTagsToProductHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ProductWithTagsDto> Handle(GetProductWithTagsQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var result = await _dbContext.Products.Include(x => x.Tags)
                .Where(x => x.Id == request.ProductId)
                .Select(x => new ProductWithTagsDto
                {
                    ProductId = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Tags = x.Tags.Select(t => new TagDto
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToArray()
                })
                .FirstAsync(cancellationToken);

            return result;
        }
    }
}
