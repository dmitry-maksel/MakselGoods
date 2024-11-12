using Products.API.Core.Entities;

namespace Products.API.Core.CQRS.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}
