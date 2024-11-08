using Identity.API.Core.Models;
using MediatR;

namespace Identity.API.Core.Queries
{
    public record LoginCommand(string Username, string Password) : IRequest<LoginResponseModel>, ILoggedRequest;
}