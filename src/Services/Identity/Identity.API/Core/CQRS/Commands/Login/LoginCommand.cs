using Identity.API.Core.Interfaces;
using Identity.API.Core.Models;
using MediatR;

namespace Identity.API.Core.CQRS.Commands.Login
{
    public record LoginCommand(string Username, string Password) : IRequest<LoginDto>, ILoggedRequest;
}