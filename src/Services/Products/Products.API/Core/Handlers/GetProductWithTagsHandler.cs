using Microsoft.EntityFrameworkCore;
using Products.API.Core.Models;
using Products.API.Core.Queries;

namespace Products.API.Core.Handlers
{
    public class GetProductWithTagsHandler : IRequestHandler<GetProductWithTagsQuery, ProductWithTagsModel>
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<AddTagsToProductHandler> _logger;

        public GetProductWithTagsHandler(ProductsDbContext dbContext, ILogger<AddTagsToProductHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ProductWithTagsModel> Handle(GetProductWithTagsQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var result = await _dbContext.Products.Include(x => x.Tags)
                .Where(x => x.Id == request.ProductId)
                .Select(x => new ProductWithTagsModel
                {
                    ProductId = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Tags = x.Tags.Select(t => new TagModel
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
