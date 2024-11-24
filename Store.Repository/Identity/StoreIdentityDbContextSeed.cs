using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Core.Identity;

namespace Store.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if ( !_userManager.Users.Any() )
            {
                var user = new AppUser()
                {
                    DisplayName = "AmmarYasser",
                    Email = "Ammaryasser@gmail.com",
                    UserName = "Ammaryasser",
                    PhoneNumber = "01026627466"
                    

                };
                // need o add this data in the data base --> i have this function in UserManager Package in Identity Package so should Inject 

                await _userManager.CreateAsync(user, "P@ssW0rd");

            }


        }
    }
}
