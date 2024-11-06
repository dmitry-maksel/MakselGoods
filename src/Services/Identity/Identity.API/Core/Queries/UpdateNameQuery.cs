using MediatR;

namespace Identity.API.Core.Queries;

public record UpdateNameQuery(int Id, string DisplayName) : IRequest<bool>;
