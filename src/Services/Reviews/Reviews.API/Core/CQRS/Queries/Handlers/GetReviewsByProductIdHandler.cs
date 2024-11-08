using MediatR;
using Reviews.API.Core.Interfaces;
using Reviews.API.Models;

namespace Reviews.API.Core.CQRS.Queries.Handlers;

public class GetReviewsByProductIdHandler : IRequestHandler<GetReviewByProductIdQuery, List<ReviewDto>>
{
    private readonly ILogger<GetReviewsByProductIdHandler> _logger;
    private readonly IReviewRepository _repository;

    public GetReviewsByProductIdHandler(ILogger<GetReviewsByProductIdHandler> logger, IReviewRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<List<ReviewDto>> Handle(GetReviewByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productExists = await _repository.ProductExists(request.ProductId, cancellationToken);

        if (!productExists)
        {
            throw new Exception($"Product with id:{request.ProductId} not found");
        }

        var reviews = await _repository.GetReviewsByProductId(request.ProductId, cancellationToken);

        return reviews;
    }
}
