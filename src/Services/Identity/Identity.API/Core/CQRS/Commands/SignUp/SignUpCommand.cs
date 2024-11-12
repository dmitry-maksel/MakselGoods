using Identity.API.Core.Interfaces;
using MediatR;

namespace Identity.API.Core.CQRS.Commands.SignUp
{
    public record SignUpCommand(
        string DisplayName,
        string UserName,
        string Email,
        string Password) : IRequest<int>, ILoggedRequest;
}
