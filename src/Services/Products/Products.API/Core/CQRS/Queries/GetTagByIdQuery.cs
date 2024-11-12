using Products.API.Core.Entities;

namespace Products.API.Core.CQRS.Queries;

public record GetTagByIdQuery(int Id) : IRequest<Tag?>;
