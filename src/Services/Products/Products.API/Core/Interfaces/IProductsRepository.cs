using Products.API.Core.Entities;
using Products.API.Core.Models;

namespace Products.API.Core.Interfaces;

public interface IProductsRepository
{
    #region Products
    Task<List<ProductDto>> GetAllProducts(CancellationToken cancellationToken);

    Task<ProductDto?> GetProductById(int id, CancellationToken cancellationToken);

    Task<int> CreateProduct(Product product, CancellationToken cancellationToken);

    Task<bool> UpdateProduct(int id, string name, string description, CancellationToken cancellationToken);

    Task<bool> DeleteProduct(int id, CancellationToken cancellationToken);

    #endregion

    #region Tags
    Task<List<TagDto>> GetAllTags(CancellationToken cancellationToken);

    Task<TagDto?> GetTagById(int id, CancellationToken cancellationToken);

    Task<List<TagDto>> GetTagsByProductId(int productId, CancellationToken cancellationToken);

    Task<int> CreateTag(Tag tag, CancellationToken cancellationToken);

    Task<bool> RemoveTag(int id, CancellationToken cancellationToken);

    Task<bool> UpdateTag(int id, string name, CancellationToken cancellationToken);
    #endregion

    #region ProductTags
    Task<List<ProductTag>> GetProductTags(int productId, CancellationToken cancellationToken);

    Task CreateProductTags(IEnumerable<ProductTag> productTags, CancellationToken cancellationToken);

    Task RemoveProductTagsByProductId(int productId, CancellationToken cancellationToken);

    Task<List<ProductWithTagsDto>> GetProductWithTags(int productId, CancellationToken cancellationToken);
    #endregion
}
