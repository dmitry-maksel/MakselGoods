using MediatR;

namespace Reviews.API.Core.Queries;

public record UpdateReviewQuery(int Id, int Rating, string Text) : IRequest<bool>;