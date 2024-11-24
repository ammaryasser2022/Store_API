using Microsoft.EntityFrameworkCore;
using Store.Core.Services.Contract;
using Store.Core;
using Store.Repository;
using Store.Repository.Data.Contexts;
using Store.Service.Services.Products;
using Store.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;
using Store.Core.Repositories.Contract;
using Store.Repository.Repositories;
using StackExchange.Redis;
using Store.Core.Mapping.Basket;
using Store.Service.Services.Cashes;
using Store.Repository.Identity.Contexts;
using Store.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Store.Service.Services.Tokens;
using Store.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Mapping.Auth;
using Store.Core.Mapping.Orders;
using Store.Service.Services.Orders;
using Store.Service.Services.Payments;

namespace Store.APIs.HelperForProg
{
    public static class DependencyInjections 
    {
        public static IServiceCollection AddDependency(this IServiceCollection services ,IConfiguration configuration )  // to be extention method for Services to call in program
        {

            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigreInvalidModelStateResponseService(configuration);
            services.AddRedisService(configuration);
            services.AddIdentityServices();
            services.AddAuthenticationServices(configuration);

            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)  // to be extention method for Services to call in program
        {

            services.AddControllers();

            return services;
        }            // *********   hndh kkol el function de in fun AddDependency then only Call it in Program 


        private static IServiceCollection AddSwaggerService(this IServiceCollection services)  // to be extention method for Services to call in program
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();           
            

            return services;
        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services , IConfiguration configuration)  // to be extention method for Services to call in program
        {

            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            });


            return services;
        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services )  // to be extention method for Services to call in program
        {


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICasheService, CasheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();



            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)  // to be extention method for Services to call in program
        {

            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile(configuration)));

            return services;
        }


        private static IServiceCollection ConfigreInvalidModelStateResponseService(this IServiceCollection services, IConfiguration configuration)  // to be extention method for Services to call in program
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //hklm el 7age elly responsible for the validation error view    // For any respone corresponding to MOdel State Errors -->InvalidModelStateResponseFactory
                options.InvalidModelStateResponseFactory = (actionConext) =>
                {
                    // hklm el action context da 3shan 3ayz kol el errors bt3t InvalidModelStateResponseFactory in this case 
                    var errors = actionConext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                            .SelectMany(P => P.Value.Errors)
                                            .Select(E => E.ErrorMessage)
                                            .ToArray();

                    var response = new ApiValidationErrorResponce()
                    {
                        Errors = errors,
                        StatusCode = 400  // MSH M7tago because it regonized by default 

                    };
                    // but here i dont have BadRequest Function So iWill Use BadRequestObjectResult 
                    //return BadRequest(response);  //--> invalid
                    return new BadRequestObjectResult(response);
                };

            });
            return services;
        }




        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)  // to be extention method for Services to call in program
        {

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);

            });


            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        { 

            services.AddIdentity<AppUser , IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext> ();    // allow DI l kol Identities

            return services;
        }           
        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))


                    };
                });
            return services;
        }           







    }
}
