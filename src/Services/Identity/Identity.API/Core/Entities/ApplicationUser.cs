using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? DisplayName { get; set; }
    }
}
