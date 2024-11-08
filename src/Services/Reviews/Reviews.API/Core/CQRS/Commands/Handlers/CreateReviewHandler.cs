using MediatR;
using Reviews.API.Core.Entities;
using Reviews.API.Core.Interfaces;

namespace Reviews.API.Core.CQRS.Commands.Handlers;

public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, int>
{
    private readonly ILogger<CreateReviewHandler> _logger;
    private readonly IReviewRepository _repository;

    public CreateReviewHandler(ILogger<CreateReviewHandler> logger, IReviewRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _repository.ProductExists(request.ProductId, cancellationToken);

        if (!productExists)
        {
            throw new Exception($"Product with id:{request.ProductId} not found");
        }

        var userExists = await _repository.UserExists(request.UserId, cancellationToken);

        if (!userExists)
        {
            throw new Exception($"User with id:{request.UserId} not found");
        }

        var review = new Review
        {
            ProductId = request.ProductId,
            UserId = request.UserId,
            IsApproved = true, // temporary
            Rating = request.Rating,
            Text = request.Text,
            CreatedAt = DateTimeOffset.UtcNow,
            ModifiedAt = DateTimeOffset.UtcNow
        };

        var id = await _repository.CreateReview(review, cancellationToken);

        return id;
    }
}
