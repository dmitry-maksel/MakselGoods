using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.API.Core.Queries.Tags;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/v1/tag")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TagController> _logger;

        public TagController(IMediator mediator, ILogger<TagController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("{action} method executed", nameof(GetAll));

            try
            {
                var result = await _mediator.Send(new GetAllTagsQuery());

                return result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Receiving tags failed");
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("{action} method executed", nameof(GetById));

            try
            {
                var result = await _mediator.Send(new GetTagByIdQuery(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Receiving the tag failed");
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagQuery query, IValidator<CreateTagQuery> validator)
        {
            _logger.LogInformation("{action} method executed", nameof(Create));

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
                _logger.LogError(ex, "Creating a new tag failed. Request body: {query}", query);
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTagQuery query, IValidator<UpdateTagQuery> validator)
        {
            _logger.LogInformation("{action} method executed", nameof(Update));

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
                _logger.LogError(ex, "Updating tag failed. Request body: {query}", query);
                throw;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("{action} method executed", nameof(Delete));

            try
            {
                var result = await _mediator.Send(new RemoveTagQuery(id));

                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Removing tag failed");
                throw;
            }
        }
    }
}
