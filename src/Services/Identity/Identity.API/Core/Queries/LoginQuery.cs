using Identity.API.Core.Models;
using MediatR;

namespace Identity.API.Core.Queries
{
    public record LoginQuery(string Username, string Password) : IRequest<LoginResponseModel>, ILoggedRequest;
}