using MediatR;

namespace Identity.API.Core.Queries
{
    public record SignUpCommand(
        string DisplayName,
        string UserName,
        string Email,
        string Password) : IRequest<int>, ILoggedRequest;
}
