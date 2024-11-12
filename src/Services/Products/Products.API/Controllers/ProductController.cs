
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.API.Core.CQRS.Commands;
using Products.API.Core.CQRS.Queries;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"{nameof(GetAll)} method executed");

            try
            {
                var result = await _mediator.Send(new GetAllProductsQuery());

                return result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Receiving all products failed");
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"{nameof(GetById)} method executed with id: {id}");

            try
            {
                var result = await _mediator.Send(new GetProductByIdQuery { Id = id });

                return result is not null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Receiving the product failed");
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand query, IValidator<CreateProductCommand> validator)
        {
            _logger.LogInformation($"{nameof(Create)} method executed");

            var validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation errors: {validationResult}", validationResult);
                return BadRequest(validationResult.ToString());
            }

            try
            {
                var id = await _mediator.Send(query);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating a new product failed. Request body: {query}", query);
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand query, IValidator<UpdateProductCommand> validator)
        {
            _logger.LogInformation($"{nameof(Update)} method executed");

            var validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation errors: {validationResult}", validationResult);
                return BadRequest(validationResult.ToString());
            }

            try
            {
                var result = await _mediator.Send(query);

                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating product failed. Request body: {query}", query);
                throw;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            _logger.LogInformation($"{nameof(Remove)} method executed with id: {id}");

            try
            {
                var result = await _mediator.Send(new RemoveProductCommand { Id = id });

                return result ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Removing product failed. Id: {id}", id);
                throw;
            }
        }
    }
}
