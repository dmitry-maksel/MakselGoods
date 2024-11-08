using Microsoft.EntityFrameworkCore;
using Reviews.API.Core.Entities;
using Reviews.API.Core.Interfaces;
using Reviews.API.Data;
using Reviews.API.Models;

namespace Reviews.API.Infrastructure.Implementaions;

public class ReviewRepository : IReviewRepository
{
    private readonly ReviewsDbContext _context;

    public ReviewRepository(ReviewsDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateReview(Review review, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(review, nameof(review));

        await _context.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return review.Id;
    }

    public async Task<List<ReviewDto>> GetReviewsByProductId(int productId, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .Where(x => x.ProductId == productId)
            .Select(x => new ReviewDto // TODO change to ProjectTo from Automapper
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
    }

    public async Task<bool> ProductExists(int productId, CancellationToken cancellationToken)
    {
        return await _context.Products.AnyAsync(x => x.Id == productId && !x.DeletedAt.HasValue, cancellationToken);
    }

    public async Task<bool> UserExists(int userId, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
    }
}
