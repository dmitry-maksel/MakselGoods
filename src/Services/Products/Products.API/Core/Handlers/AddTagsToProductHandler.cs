using Microsoft.EntityFrameworkCore;
using Products.API.Core.Queries;

namespace Products.API.Core.Handlers
{
    public class AddTagsToProductHandler : IRequestHandler<AddTagsToProductQuery, bool>
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<AddTagsToProductHandler> _logger;

        public AddTagsToProductHandler(ProductsDbContext dbContext, ILogger<AddTagsToProductHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Handle(AddTagsToProductQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var productTags = await _dbContext.ProductsTags.Where(x => x.ProductId == request.ProductId).ToArrayAsync(cancellationToken);

            _dbContext.RemoveRange(productTags);

            var newProductTags = request.TagIds.Select(x => new ProductTag { ProductId = request.ProductId, TagId = x });

            await _dbContext.AddRangeAsync(newProductTags);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
