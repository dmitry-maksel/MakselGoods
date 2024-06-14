using Identity.API.Core.Data;

namespace Identity.API.Infrastructure.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
