using Products.API.Core.Models;

namespace Products.API.Core.Queries
{
    public class GetProductWithTagsQuery : IRequest<ProductWithTagsModel>
    {
        public int ProductId { get; set; }
    }
}
