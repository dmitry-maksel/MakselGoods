using Products.API.Core.Models;

namespace Products.API.Core.CQRS.Queries
{
    public class GetProductWithTagsQuery : IRequest<ProductWithTagsDto>
    {
        public int ProductId { get; set; }
    }
}
