using MediatR;

namespace Identity.API.Core.Queries
{
    public record SignUpQuery(
        string DisplayName,
        string UserName,
        string Email,
        string Password) : IRequest<int>;
}
