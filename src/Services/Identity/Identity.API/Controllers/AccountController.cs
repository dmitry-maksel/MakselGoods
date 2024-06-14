using FluentValidation;
using Identity.API.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery, IValidator<LoginQuery> validator)
        {
            _logger.LogInformation($"{DateTime.UtcNow} || {nameof(Login)} started");

            var validationResult = validator.Validate(loginQuery);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning($"Validation errors: {validationResult}");
                return BadRequest(validationResult.ToString());
            }

            try
            {
                var result = await _mediator.Send(loginQuery);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Login process: {loginQuery}", loginQuery);
                throw;
            }
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpQuery signUpQuery, IValidator<SignUpQuery> validator)
        {
            _logger.LogInformation($"{DateTime.UtcNow} || {nameof(SignUp)} started");

            var validationResult = validator.Validate(signUpQuery);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning($"Validation errors: {validationResult}");
                return BadRequest(validationResult.ToString());
            }

            try
            {
                var id = await _mediator.Send(signUpQuery);

                return id > 0 ? Ok(id) : BadRequest($"{nameof(SignUp)} failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Login process: {loginQuery}", signUpQuery);
                throw;
            }
        }
    }
}
