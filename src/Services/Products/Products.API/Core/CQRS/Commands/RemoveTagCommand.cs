namespace Products.API.Core.CQRS.Commands;

public record RemoveTagCommand(int Id) : IRequest<bool>;
