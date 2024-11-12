using Identity.API.Core.Entities;

namespace Identity.API.Core.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
