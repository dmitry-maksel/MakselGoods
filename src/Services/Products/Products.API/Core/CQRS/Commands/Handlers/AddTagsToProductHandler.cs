using Products.API.Core.Entities;
using Products.API.Core.Interfaces;

namespace Products.API.Core.CQRS.Commands.Handlers
{
    public class AddTagsToProductHandler : IRequestHandler<AddTagsToProductCommand, bool>
    {
        private readonly IProductsRepository _repository;
        private readonly ILogger<AddTagsToProductHandler> _logger;

        public AddTagsToProductHandler(IProductsRepository repository, ILogger<AddTagsToProductHandler> logger)
        {

            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(AddTagsToProductCommand request, CancellationToken cancellationToken)
        {
            //TODO check if product Exists and tags exist

            await _repository.RemoveProductTagsByProductId(request.ProductId, cancellationToken);

            var newProductTags = request.TagIds.Select(id => new ProductTag { ProductId = request.ProductId, TagId = id });

            await _repository.CreateProductTags(newProductTags, cancellationToken);

            return true;
        }
    }
}
