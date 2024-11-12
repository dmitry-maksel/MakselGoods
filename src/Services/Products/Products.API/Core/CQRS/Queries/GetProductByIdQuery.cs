using Products.API.Core.Entities;

namespace Products.API.Core.CQRS.Queries
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int Id { get; set; }
    }
}
