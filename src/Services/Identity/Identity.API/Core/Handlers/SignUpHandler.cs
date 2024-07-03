using Identity.API.Core.Data;
using Identity.API.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core.Handlers
{
    public class SignUpHandler : IRequestHandler<SignUpQuery, int>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SignUpHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> Handle(SignUpQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                throw new Exceptions.ApplicationException("Email already exist");
            }

            user = await _userManager.FindByNameAsync(request.UserName);

            if (user is not null)
            {
                throw new Exceptions.ApplicationException("UserName already exist");
            }

            user = new ApplicationUser
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded) return user.Id;

            throw new Exceptions.ApplicationException("SignUp process failed");
        }
    }
}
