using MediatR;
using Reviews.API.Core.Models;

namespace Reviews.API.Core.Queries;
public record GetReviewByProductIdQuery(int ProductId) : IRequest<List<GetReviewResponseModel>>;