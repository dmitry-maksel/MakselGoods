using MediatR;
using Reviews.API.Models;

namespace Reviews.API.Core.CQRS.Queries;
public record GetReviewByProductIdQuery(int ProductId) : IRequest<List<ReviewDto>>;