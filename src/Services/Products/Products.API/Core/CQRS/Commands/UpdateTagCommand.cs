namespace Products.API.Core.CQRS.Commands;

public record UpdateTagCommand(int Id, string Name) : IRequest<bool>;
