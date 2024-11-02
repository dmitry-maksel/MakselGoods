using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reviews.API.Core.Models;
using Reviews.API.Core.Queries;

namespace Reviews.API.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductReviewsController> _logger;

        public ProductReviewsController(IMediator mediator, ILogger<ProductReviewsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{productId}/reviews")]
        public async Task<IActionResult> GetReviewsByProduct(int productId, CancellationToken cancellationToken)
        {
            var reviews = await _mediator.Send(new GetReviewByProductIdQuery(productId), cancellationToken);

            return reviews.Count != 0 ? Ok(reviews) : NotFound();
        }

        [HttpPost("{productId}/reviews")]
        public async Task<IActionResult> Create(int productId, [FromBody] CreateReviewRequestModel model, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateReviewQuery(productId, model.UserId, model.Rating, model.Text), cancellationToken);

            return Ok(id);
        }

        //[HttpPut("{productId}/reviews")]
        //public async Task<IActionResult> Update(int productId, [FromBody] UpdateReviewQuery query, CancellationToken cancellationToken)
        //{
        //    var created = await _mediator.Send(query, cancellationToken);

        //    return created ? Ok() : BadRequest();
        //}

        //[HttpDelete("{productId}/reviews")]
        //public async Task<IActionResult> Delete(int productId, CancellationToken cancellationToken)
        //{
        //    var deleted = await _mediator.Send(new DeleteReviewQuery(productId), cancellationToken);

        //    return deleted ? Ok() : BadRequest();
        //}
    }
}
