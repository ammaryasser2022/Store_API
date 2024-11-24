using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Services.Contract;
using Store.Service.Services.Cashes;

namespace Store.APIs.Attributes
{
    public class CasheAttribute : Attribute , IAsyncActionFilter //-> To make request chech here first  
    {
        private readonly int _expireTime;

        public CasheAttribute(int expireTime) 
        { 
            _expireTime = expireTime;
              
        }
        

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) // de hhnfz before main fun in controller
                                                                                                        // to chech if there cashed data or not 
          {
            // chech if there data Cashed in InMemoryDb
            // but i dont craete it in ctor because when doing that when call [Cashe] i should send object from ICasheService
            // i will create with this way  
            var casheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();

            // To Search In InMemoryDb Ineed Key 
            // Ineed A View to Key For each Type Of data like ( all product with Pafination ,  all product , all product page index ,...
            // so i will make Private Fun --

            var cacheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse =await casheService.GetCasheKeyAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))  // lw el request da  mawgood in InMemoryDb :) --->
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = contentResult;
                return; //stop ll fun 

            }
            else
            {
                // m7tag anfz GetAllProducts in the ProductController ---> Note : Ihave the adress of this fun in next delegate 
                var excutedContext = await next.Invoke();

                //then check if the result from GetAllProducts is Ok --> store in response (new syntax) ** Important 
                if (excutedContext.Result is OkObjectResult response )
                {
                    await casheService.SetCasheKeyAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_expireTime));
                }
            }

        }

        // bdeha shkl el path
        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            var casheKey = new StringBuilder();
            casheKey.Append($"{request.Path}");
            // add path of request

            // check Query params in each path
            foreach(var (Key , Value)  in request.Query.OrderBy(X=>X.Key))
            {
                casheKey.Append($"{Key} - {Value}"); 
            }
            return casheKey.ToString();
        }
    }
}
