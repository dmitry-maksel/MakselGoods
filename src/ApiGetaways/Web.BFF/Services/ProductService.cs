using Web.BFF.Models;

namespace Web.BFF.Services;

public class ProductService : IProductService
{
    private readonly Reviews.ReviewsClient _reviewsClient;

    public ProductService(Reviews.ReviewsClient reviewsClient)
    {
        _reviewsClient = reviewsClient;
    }

    public async Task<ProductDetails> GetProductDetails(int productId, CancellationToken cancellationToken)
    {
        var reviews = await GetReviews(productId, cancellationToken);
        var details = new ProductDetails(null, null, reviews);

        return details;
    }

    private async Task<List<Review>> GetReviews(int productId, CancellationToken cancellationToken)
    {
        var request = new GetReviewsByProductIdRequest { ProductId = productId };
        var call = await _reviewsClient.GetReviewsByProductIdAsync(request);

        var reviews = new List<Review>(call.Reviews.Select(r => new Review(
                r.Id, r.ProductId, r.UserId, r.UserDisplayName,r.Rating, r.Text,
                r.CreatedAt.ToDateTimeOffset(), r.ModifiedAt.ToDateTimeOffset())));

        return reviews;
    }
}
