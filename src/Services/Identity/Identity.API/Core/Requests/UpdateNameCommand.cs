using MediatR;

namespace Identity.API.Core.Queries;

public record UpdateNameCommand(int Id, string DisplayName) : IRequest<bool>;
