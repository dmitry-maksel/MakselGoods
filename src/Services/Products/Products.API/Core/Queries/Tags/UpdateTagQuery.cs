namespace Products.API.Core.Queries.Tags;

public record UpdateTagQuery(int Id, string Name) : IRequest<bool>;
