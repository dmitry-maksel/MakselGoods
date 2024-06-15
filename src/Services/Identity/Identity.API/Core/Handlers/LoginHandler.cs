using Identity.API.Core.Data;
using Identity.API.Core.Exceptions;
using Identity.API.Core.Models;
using Identity.API.Core.Queries;
using Identity.API.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core.Handlers
{
    public class LoginHandler : IRequestHandler<LoginQuery, LoginResponseModel>
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
        public async Task<LoginResponseModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new IdentityException("User not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) throw new IdentityException("Invalid username or password");

            var token = _tokenGenerator.GenerateToken(user);

            return new LoginResponseModel(user.Id, token);
        }
    }
}
