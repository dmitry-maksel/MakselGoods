using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? DisplayName { get; set; }
    }
}
