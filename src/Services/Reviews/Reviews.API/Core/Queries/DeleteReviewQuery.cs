using MediatR;

namespace Reviews.API.Core.Queries;

public record DeleteReviewQuery(int ProductId) : IRequest<bool>;
