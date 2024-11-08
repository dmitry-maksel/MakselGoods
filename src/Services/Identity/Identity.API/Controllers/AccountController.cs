using FluentValidation;
using Identity.API.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
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
        public async Task<IActionResult> Login([FromBody] LoginCommand loginQuery, IValidator<LoginCommand> validator)
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
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand signUpQuery, IValidator<SignUpCommand> validator)
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

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Login process: {loginQuery}", signUpQuery);
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string displayName, CancellationToken cancellationToken)
        {
            var idClaim = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);
            var id = int.Parse(idClaim.Value);

            var result = await _mediator.Send(new UpdateNameCommand(id, displayName), cancellationToken);

            return result ? Ok() : BadRequest();
        }

        [HttpPost("testauth")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
