namespace Products.API.Core.CQRS.Commands
{
    public class RemoveProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
