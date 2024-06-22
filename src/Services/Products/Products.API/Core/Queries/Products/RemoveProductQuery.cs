namespace Products.API.Core.Queries.Products
{
    public class RemoveProductQuery : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
