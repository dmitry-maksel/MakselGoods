namespace Products.API.Core.CQRS.Commands
{
    public class AddTagsToProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }

        public int[] TagIds { get; set; } = []!;
    }
}
