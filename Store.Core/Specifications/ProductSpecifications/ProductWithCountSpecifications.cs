using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Specifications.ProductSpecifications
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecifications(ProductSpecParams productSpec) : base(
               /*string? sort , int? brandId, int? typeId , int pageSize, int pageIndex*/

               P =>
                   //lw b null y3ni first part true            ---> msh hakhosh b3d el &&
                   (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
                   &&
                   (!productSpec.BrandId.HasValue || productSpec.BrandId == P.BrandId)
                   &&
                   (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId))

        {
            

        }
    }
}
