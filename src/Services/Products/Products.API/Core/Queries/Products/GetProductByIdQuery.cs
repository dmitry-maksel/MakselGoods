namespace Products.API.Core.Queries.Products
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int Id { get; set; }
    }
}
