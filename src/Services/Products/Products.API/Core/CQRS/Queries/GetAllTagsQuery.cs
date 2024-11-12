using Products.API.Core.Entities;

namespace Products.API.Core.CQRS.Queries;

public record GetAllTagsQuery() : IRequest<List<Tag>>;
