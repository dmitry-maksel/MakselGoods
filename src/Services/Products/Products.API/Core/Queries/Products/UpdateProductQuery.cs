namespace Products.API.Core.Queries.Products
{
    public class UpdateProductQuery : IRequest<bool>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
