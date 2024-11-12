namespace Products.API.Core.CQRS.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
