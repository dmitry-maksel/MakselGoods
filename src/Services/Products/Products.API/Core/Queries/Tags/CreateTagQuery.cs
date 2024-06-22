namespace Products.API.Core.Queries.Tags;

public record CreateTagQuery(string Name) : IRequest<int>;
