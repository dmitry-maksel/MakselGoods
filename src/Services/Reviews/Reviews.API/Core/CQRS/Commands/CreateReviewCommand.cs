using MediatR;

namespace Reviews.API.Core.CQRS.Commands;

public record CreateReviewCommand(int ProductId, int UserId, int Rating, string Text) : IRequest<int>;
