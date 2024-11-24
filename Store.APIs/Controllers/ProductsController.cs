using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Attributes;
using Store.APIs.Error;
using Store.Core.Dtos.Product;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications.ProductSpecifications;

namespace Store.APIs.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : /*ControllerBase*/ BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [ProducesResponseType(typeof(PaginationResponse<ProductDto> ), StatusCodes.Status200OK)]
        [HttpGet]  // url /endpoint ----> Get BaseUrl/api/Products
                   //noteeee name of end point msh b7tagoo(GetAllProducts)

        // sort : name or  ptice asc or price des 
        [Cashe(100)]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec ) /*string? sort , [FromQuery]int ? brandId , [FromQuery] int? typeId, [FromQuery] int? pageSize = 5 , [FromQuery] int? pageIndex = 1)*/ //end point  
        {
            // Ineed the fun who is in Servie --> mean i need object fromo ProductServive

            var result = await _productService.GetAllProductsAsync(productSpec/*sort, brandId ,typeId , pageSize , pageIndex*/ );

            // note no return view 
            return Ok(result);    // ststus code 200
           //return Ok(new PaginationResponse<ProductDto>(productSpec.PageSize , productSpec.PageIndex , 0 , resu   lt));  // Will run but i dont
                                                                                                                       // need write loic here 
                                                                                                                       //write in GetAllProductsAsync
        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        //[HttpGet]  // Get BaseUrl/api/Products    --> clr fine but swagger annoyed  // lazem a8er url 3n elly fo2
        [HttpGet("brands")] //Get BaseUrl/api/Products/brands
        [Authorize]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GettAllBrands() //endPOint 
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);

        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)] 
        [HttpGet("types")] //Get BaseUrl/api/Products/types 
        [Authorize]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GettAllTypes() //endPOint 
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);

        }



        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]  //BaseUrl/api/Products

        public async Task<IActionResult> GettProductsById(int? id)
        {
            if (id == null) return BadRequest(new ApiErrorResponse(400));
            var result = await _productService.GetProductById(id.Value);
            if (result == null) return NotFound(new ApiErrorResponse(404));

            return Ok(result);
        }
    }
}
