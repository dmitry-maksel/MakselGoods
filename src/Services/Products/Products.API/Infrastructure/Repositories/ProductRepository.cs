using Microsoft.EntityFrameworkCore;
using Products.API.Core.Entities;
using Products.API.Core.Interfaces;
using Products.API.Core.Models;
using Products.API.Data;

namespace Products.API.Infrastructure.Repositories;

public class ProductRepository : IProductsRepository
{
    private readonly ProductsDbContext _context;

    public ProductRepository(ProductsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddTagToProduct(int productId, int[] tagIds, CancellationToken cancellationToken)
    {
        var productTags = await _context.ProductsTags.Where(x => x.ProductId == productId).ToArrayAsync(cancellationToken);
        _context.RemoveRange(productTags);
        var newProductTags = tagIds.Select(id => new ProductTag { ProductId = productId, TagId = id });

        _context.AddRange(newProductTags, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<int> CreateProduct(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task CreateProductTags(IEnumerable<ProductTag> productTags, CancellationToken cancellationToken)
    {
        _context.ProductsTags.AddRange(productTags);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CreateTag(Tag tag, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteProduct(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductDto>> GetAllProducts(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TagDto>> GetAllTags(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDto?> GetProductById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductTag>> GetProductTags(int productId, CancellationToken cancellationToken)
    {
        var productTags = await _context.ProductsTags.Where(x => x.ProductId == productId).ToListAsync(cancellationToken);

        return productTags;
    }

    public async Task<List<ProductWithTagsDto>> GetProductWithTags(int productId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TagDto?> GetTagById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveProductTagsByProductId(int productId, CancellationToken cancellationToken)
    {
        var productTags = await _context.ProductsTags.Where(x => x.ProductId == productId).ToListAsync(cancellationToken);

        _context.RemoveRange(productTags);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> RemoveTag(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateProduct(int id, string name, string description, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateTag(int id, string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
