using MediatR;

namespace Reviews.API.Core.CQRS.Commands;

public record UpdateReviewCommand(int Id, int Rating, string Text) : IRequest<bool>;