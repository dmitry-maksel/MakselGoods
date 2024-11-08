using Reviews.API.Core.Entities;
using Reviews.API.Models;

namespace Reviews.API.Core.Interfaces;

public interface IReviewRepository
{
    Task<bool> ProductExists(int productId, CancellationToken cancellationToken);

    Task<bool> UserExists(int userId, CancellationToken cancellationToken);

    Task<List<ReviewDto>> GetReviewsByProductId(int productId, CancellationToken cancellationToken);

    Task<int> CreateReview(Review review, CancellationToken cancellationToken);
}
