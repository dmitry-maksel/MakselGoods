namespace Products.API.Core.Queries
{
    public class AddTagsToProductQuery : IRequest<bool>
    {
        public int ProductId { get; set; }

        public int[] TagIds { get; set; } = []!;
    }
}
