using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;

namespace Store.APIs.Controllers
{
    [Route("error")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]  // Ignore this controller in Swagger 

    // this class for not found end point error 
    //this class work when send request dont match any endpoint  
    // this Done By Configre Middleware -->  UseStatusCodePagesWithReExecute 
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error() //status code 
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound , "Not Fount End Point :(")   );
        }
    }
}
