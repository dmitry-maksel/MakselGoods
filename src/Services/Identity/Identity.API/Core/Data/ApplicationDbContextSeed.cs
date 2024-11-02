using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        DisplayName = "Admin",
                        UserName = "admin_account",
                        Email = "admin@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "QWerty123!@#");
                }
            }
        }
    }
}
