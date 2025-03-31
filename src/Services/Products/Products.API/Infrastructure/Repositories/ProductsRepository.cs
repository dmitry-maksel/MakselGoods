using Microsoft.EntityFrameworkCore;
using Products.API.Core.Entities;
using Products.API.Core.Interfaces;
using Products.API.Core.Models;
using Products.API.Data;

namespace Products.API.Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly ProductsDbContext _context;

    public ProductsRepository(ProductsDbContext context)
    {
        _context = context;
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
        var product = await _context.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                ModifiedAt = p.ModifiedAt,
                DeletedAt = p.DeletedAt
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return product;
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

    public async Task<List<TagDto>> GetTagsByProductId(int productId, CancellationToken cancellationToken)
    {
        var tags = await _context.ProductsTags
            .Where(x => x.ProductId == productId)
            .Select(x => new TagDto
            {
                Id = x.TagId,
                Name = x.Tag.Name
            })
            .ToListAsync(cancellationToken);

        return tags;
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
