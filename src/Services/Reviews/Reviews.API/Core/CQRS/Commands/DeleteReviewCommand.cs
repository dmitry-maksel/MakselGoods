using MediatR;

namespace Reviews.API.Core.CQRS.Commands;

public record DeleteReviewCommand(int ProductId) : IRequest<bool>;
