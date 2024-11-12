using Microsoft.AspNetCore.Mvc;
using Web.BFF.Services;

namespace Web.BFF.Controllers;

[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProductDetails(int productId, CancellationToken cancellationToken)
    {
        var details = await _productService.GetProductDetails(productId, cancellationToken);

        return Ok(details);
    }
}
