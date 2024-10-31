using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.API.Core.Queries;

namespace Products.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1")]
    public class ProductsTagsController : ControllerBase
    {
        private readonly ILogger<ProductsTagsController> _logger;
        private readonly IMediator _mediator;

        public ProductsTagsController(ILogger<ProductsTagsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("product/add-tags")]
        public async Task<IActionResult> AssignTagsToProduct(AddTagsToProductQuery query, IValidator<AddTagsToProductQuery> validator, CancellationToken cancellationToken)
        {
            var validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation errors: {validationResult}", validationResult);
                return BadRequest(validationResult.ToString());
            }

            try
            {
                var result = await _mediator.Send(query, cancellationToken);

                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred while adding tags to product");
                throw;
            }
        }

        [HttpGet("product/{productId}/tags")]
        public async Task<IActionResult> GetProductWithTags(int productId)
        {
            try
            {
                var result = await _mediator.Send(new GetProductWithTagsQuery { ProductId = productId });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred while recieving product with tags");
                throw;
            }
        }
    }
}
