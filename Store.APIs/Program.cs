
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.APIs.Error;
using Store.APIs.HelperForProg;
using Store.APIs.Middelwares;
using Store.Core;
using Store.Core.Mapping.Products;
using Store.Core.Services.Contract;
using Store.Repository;
using Store.Repository.Data;
using Store.Repository.Data.Contexts;
using Store.Service.Services.Products;

namespace Store.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

             
            builder.Services.AddDependency(builder.Configuration);
            #region Before HekperForPrg Folder
            //builder.Services.AddBuiltInService();
            //builder.Services.AddControllers();   // add built in service for APIs



            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();                      ----->Moved to AddSwagger SERVICE 


            //builder.Services.AddDbContext<StoreDbContext>(option =>
            //{
            //    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            //});

            //builder.Services.AddScoped<IProductService, ProductService>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();   // /Moved To AddUserDefinedService



            //builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile(builder.Configuration)));


            //builder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    //hklm el 7age elly responsible for the validation error view    // For any respone corresponding to MOdel State Errors -->InvalidModelStateResponseFactory
            //    options.InvalidModelStateResponseFactory = (actionConext) =>
            //    {
            //        // hklm el action context da 3shan 3ayz kol el errors bt3t InvalidModelStateResponseFactory in this case 
            //        var errors = actionConext.ModelState.Where(P => P.Value.Errors.Count() > 0)
            //                                .SelectMany(P => P.Value.Errors)
            //                                .Select(E => E.ErrorMessage)
            //                                .ToArray();

            //        var response = new ApiValidationErrorResponce()
            //        {
            //            Errors = errors,
            //            StatusCode = 400  // MSH M7tago because it regonized by default 

            //        };
            //        // but here i dont have BadRequest Function So iWill Use BadRequestObjectResult 
            //        //return BadRequest(response);  //--> invalid
            //        return new BadRequestObjectResult(response);
            //    };

            //}); 
            #endregion


            var app = builder.Build();


            #region Before HelperForProg

            ////2.
            //// Apply Migration (Update DataBase)
            //using var scope = app.Services.CreateScope(); // btrg3 kol Scoped --> StoreDbContext
            //var services = scope.ServiceProvider;  // btrg3 el services -->StoreDbContext
            //var context = services.GetRequiredService<StoreDbContext>();  // ask for object 
            //var loggerFactory = services.GetRequiredService<ILoggerFactory>();  // ask for object 

            //try
            //{
            //    await context.Database.MigrateAsync();  // Db Created 
            //    //ADD DATA THAT I HAVE SEEDED
            //    await StoreDbContextSeed.SeedAsync(context);    
            // }
            //catch (Exception ex)
            //{
            //    //Console.WriteLine(ex);     ORRR
            //    var logger = loggerFactory.CreateLogger<Program>();
            //    logger.LogError(ex, "There Are Problems During Apply Migrations ");

            //}





            ////1.
            //// Apply Migration (Update DataBase)
            ////StoreDbContext context = new StoreDbContext();  // i need CLR crate the object 
            ////context.Database.MigrateAsync(); // Create  DB &&  applly any Migration 




            //app.UseMiddleware<ExceptionMiddleware>();  // Configre user defined middelwaree

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseStatusCodePagesWithReExecute("/error");

            //app.UseStaticFiles(); // for view satic files in response // like pics 

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            //app.MapControllers(); 
            #endregion

            await app.ConfigureMiddlewareAsync(); 

            app.Run();
        }
    }
}
