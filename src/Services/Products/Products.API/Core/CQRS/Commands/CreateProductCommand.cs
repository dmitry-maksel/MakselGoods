namespace Products.API.Core.CQRS.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
