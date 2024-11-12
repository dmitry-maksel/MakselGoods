using MediatR;

namespace Identity.API.Core.CQRS.Commands.UpdateName;

public record UpdateNameCommand(int Id, string DisplayName) : IRequest<bool>;
