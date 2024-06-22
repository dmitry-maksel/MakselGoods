namespace Products.API.Core.Queries.Products
{
    public class CreateProductQuery : IRequest<int>
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
