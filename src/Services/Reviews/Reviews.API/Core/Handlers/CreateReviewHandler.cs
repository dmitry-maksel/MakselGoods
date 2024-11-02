using MediatR;
using Microsoft.EntityFrameworkCore;
using Reviews.API.Core.Data;
using Reviews.API.Core.Queries;

namespace Reviews.API.Core.Handlers;

public class CreateReviewHandler : IRequestHandler<CreateReviewQuery, int>
{
    private readonly ILogger<GetReviewsByProductIdHandler> _logger;
    private readonly ReviewsDbContext _reviewsDbContext;

    public CreateReviewHandler(ILogger<GetReviewsByProductIdHandler> logger, ReviewsDbContext reviewsDbContext)
    {
        _logger = logger;
        _reviewsDbContext = reviewsDbContext;
    }

    public async Task<int> Handle(CreateReviewQuery request, CancellationToken cancellationToken)
    {
        var productExists = await _reviewsDbContext.Products.AnyAsync(x => x.Id == request.ProductId && !x.DeletedAt.HasValue, cancellationToken);

        if (!productExists)
        {
            throw new Exception($"Product with id:{request.ProductId} not found");
        }

        var userExists = await _reviewsDbContext.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken);

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

        _reviewsDbContext.Reviews.Add(review);
        await _reviewsDbContext.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}
