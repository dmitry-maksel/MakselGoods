using Identity.API.Core.Entities;
using Identity.API.Core.Interfaces;
using Identity.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core.CQRS.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginHandler(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenGenerator tokenGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new Exceptions.ApplicationException("User not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) throw new Exceptions.ApplicationException("Invalid username or password");

            var token = _tokenGenerator.GenerateToken(user);

            return new LoginDto(token);
        }
    }
}
