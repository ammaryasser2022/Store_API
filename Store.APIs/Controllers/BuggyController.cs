using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;
using Store.Repository.Data.Contexts;

namespace Store.APIs.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BuggyController : /*ControllerBase*/   BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }


        [HttpGet("notfound")] // Get/BASEURL/api/Buggy/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {

           var brand =await _context.Brands.FindAsync(100);
            if (brand == null) return NotFound(new ApiErrorResponse(404 , " Brand With Id 100 is Not Found"));

            return Ok(brand);

        }




        [HttpGet("servererror")] // Get/BASEURL/api/Buggy/servererror
        public async Task<IActionResult> GetServerRequestRequestError()
        {

            var brand = await _context.Brands.FindAsync(100);
            var brandToSrtring = brand.ToString(); // brand == null --> NullRefException

            return Ok(brand);

        }

         


        [HttpGet("badrequest")] // Get/BASEURL/api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {

            return BadRequest(new ApiErrorResponse(400 )); 

        }



        // ** Hna msh hy5sh in EndPoint aslnnn
        [HttpGet("badrequest/{id}")] // Get/BASEURL/api/Buggy/badrequest/ahmed      ---> ahmed string not int --> validation error  
        public async Task<IActionResult> GetBadRequestError(int id) // validation error
        {
            // da sho8l Model State 
            // but if we do tis 
            //Still Donte enter this : 
            
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResponse(400));
            }
         //***//Because I have a speacial Service has role to show the response view in case of Validation Errror This Service with Model State ***
            // so i need to change in This Service To return My View Response 


            return Ok();

        }



        [HttpGet("unauthorizederror")] // Get/BASEURL/api/Buggy/unauthorizederror     
        public async Task<IActionResult> GetUnAuthorizedError() // unauthorizederror
        {

            return Unauthorized(new ApiErrorResponse(401));

        }



    }
}
