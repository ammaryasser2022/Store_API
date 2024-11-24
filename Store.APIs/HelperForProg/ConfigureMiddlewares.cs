using Store.APIs.Middelwares;
using Store.Repository.Data.Contexts;
using Store.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Store.Repository.Identity.Contexts;
using Store.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Store.Core.Identity;

namespace Store.APIs.HelperForProg
{
    public static class ConfigureMiddlewares
    {
        public static async Task<WebApplication> ConfigureMiddlewareAsync (this WebApplication app)
        {

            //2.
            // Apply Migration (Update DataBase)
            using var scope = app.Services.CreateScope(); // btrg3 kol Scoped --> StoreDbContext
            var services = scope.ServiceProvider;  // btrg3 el services -->StoreDbContext
            var context = services.GetRequiredService<StoreDbContext>();  // ask for object 
            var IdentityContext = services.GetRequiredService<StoreIdentityDbContext>();  // ask for object 
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();  // ask for object 

            try
            {
                await context.Database.MigrateAsync();  // Db Created 
                //ADD DATA THAT I HAVE SEEDED
                await StoreDbContextSeed.SeedAsync(context);

                await IdentityContext.Database.MigrateAsync();  // Db Created  for identity 
                var userManager = services.GetRequiredService<UserManager<AppUser>>(); // de need Allow Service 
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager); // ask clr ygeb object from usermanger


            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);     ORRR
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problems During Apply Migrations ");

            }

            //1.
            // Apply Migration (Update DataBase)
            //StoreDbContext context = new StoreDbContext();  // i need CLR crate the object 
            //context.Database.MigrateAsync(); // Create  DB &&  applly any Migration 




            app.UseMiddleware<ExceptionMiddleware>();  // Configre user defined middelwaree

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error");

            app.UseStaticFiles(); // for view satic files in response // like pics 

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

    }
}
