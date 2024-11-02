using MediatR;

namespace Reviews.API.Core.Queries;

public record CreateReviewQuery(int ProductId, int UserId, int Rating, string Text) : IRequest<int>;
