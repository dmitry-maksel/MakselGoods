namespace Products.API.Core.Queries.Tags;

public record RemoveTagQuery(int Id) : IRequest<bool>;
