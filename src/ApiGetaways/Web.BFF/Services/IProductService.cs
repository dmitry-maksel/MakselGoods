using Web.BFF.Models;

namespace Web.BFF.Services;

public interface IProductService
{
    Task<ProductDetails> GetProductDetails(int  productId, CancellationToken cancellationToken);
}
