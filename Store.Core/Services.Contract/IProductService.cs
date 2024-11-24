using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Dtos.Product;
using Store.Core.Entities;
using Store.Core.Helper;
using Store.Core.Specifications.ProductSpecifications;

namespace Store.Core.Services.Contract
{
    public interface IProductService
    {
        Task</*IEnumerable<ProductDto>*/PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec /*string? sort, int? brandId ,  int? typeId, int?  pageSize , int?  pageIndex*/ );
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductById(int id);
    }
}
