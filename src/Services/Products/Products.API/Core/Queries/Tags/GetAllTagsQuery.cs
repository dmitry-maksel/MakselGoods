namespace Products.API.Core.Queries.Tags;

public record GetAllTagsQuery() : IRequest<List<Tag>>;
