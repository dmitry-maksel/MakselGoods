namespace Products.API.Core.CQRS.Commands;

public record CreateTagCommand(string Name) : IRequest<int>;
