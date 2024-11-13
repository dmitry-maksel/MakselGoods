using Web.BFF.Models;
using Web.BFF.Protos;

namespace Web.BFF.Services;

public class ProductService : IProductService
{
    private readonly GrpcReviews.GrpcReviewsClient _reviewsClient;
    private readonly GrpcProducts.GrpcProductsClient _productsClient;

    public ProductService(GrpcReviews.GrpcReviewsClient reviewsClient, GrpcProducts.GrpcProductsClient productsClient)
    {
        _reviewsClient = reviewsClient;
        _productsClient = productsClient;
    }

    public async Task<ProductDetails> GetProductDetails(int productId, CancellationToken cancellationToken)
    {
        var product = await GetProduct(productId);
        var tags = await GetTags(productId);
        var reviews = await GetReviews(productId);
        var details = new ProductDetails(product, tags, reviews);

        return details;
    }

    private async Task<Product> GetProduct(int productId)
    {
        var request = new GetProductRequest { ProductId = productId };
        var call = await _productsClient.GetProductAsync(request);

        return new Product(call.Id, call.Name, call.Description);
    }

    private async Task<List<Tag>> GetTags(int productId)
    {
        var request = new GetTagsByProductIdRequest { ProductId = productId };
        var call = await _productsClient.GetTagsByProductIdAsync(request);

        var tags = new List<Tag>(call.Tags.Select(t => new Tag(t.Id,t.Name)));

        return tags;
    }

    private async Task<List<Review>> GetReviews(int productId)
    {
        var request = new GetReviewsByProductIdRequest { ProductId = productId };
        var call = await _reviewsClient.GetReviewsByProductIdAsync(request);

        var reviews = new List<Review>(call.Reviews.Select(r => new Review(
                r.Id, r.ProductId, r.UserId, r.UserDisplayName,r.Rating, r.Text,
                r.CreatedAt.ToDateTimeOffset(), r.ModifiedAt.ToDateTimeOffset())));

        return reviews;
    }
}
