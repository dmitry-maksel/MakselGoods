using MediatR;
using Reviews.API.Models;

namespace Reviews.API.Core.Queries;
public record GetReviewByProductIdQuery(int ProductId) : IRequest<List<GetReviewResponseModel>>;