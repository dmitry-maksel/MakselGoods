namespace Products.API.Core.Queries.Tags;

public record GetTagByIdQuery(int Id) : IRequest<Tag?>;
