using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Core.Identity;

namespace Store.APIs.Exentions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email == null) return null;

            var user = await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email == Email) ;
            if (user == null) return null;

            return user;
        }
    }
}
