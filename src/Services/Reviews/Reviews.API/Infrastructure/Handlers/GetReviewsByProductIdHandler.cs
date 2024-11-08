using MediatR;
using Microsoft.EntityFrameworkCore;
using Reviews.API.Core.Queries;
using Reviews.API.Data;
using Reviews.API.Models;

namespace Reviews.API.Infrastructure.Handlers;

public class GetReviewsByProductIdHandler : IRequestHandler<GetReviewByProductIdQuery, List<GetReviewResponseModel>>
{
    private readonly ILogger<GetReviewsByProductIdHandler> _logger;
    private readonly ReviewsDbContext _reviewsDbContext;

    public GetReviewsByProductIdHandler(ILogger<GetReviewsByProductIdHandler> logger, ReviewsDbContext reviewsDbContext)
    {
        _logger = logger;
        _reviewsDbContext = reviewsDbContext;
    }

    public async Task<List<GetReviewResponseModel>> Handle(GetReviewByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productExists = await _reviewsDbContext.Products.AnyAsync(x => x.Id == request.ProductId && !x.DeletedAt.HasValue, cancellationToken);

        if (!productExists)
        {
            throw new Exception($"Product with id:{request.ProductId} not found");
        }

        var reviews = await _reviewsDbContext.Reviews
            .Where(x => x.ProductId == request.ProductId)
            .Select(x => new GetReviewResponseModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UserId = x.UserId,
                DisplayName = x.User.DisplayName,
                Rating = x.Rating,
                Text = x.Text,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            })
            .ToListAsync(cancellationToken);

        return reviews;
    }
}
