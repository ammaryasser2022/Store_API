using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Store.Core.Entities;

namespace Store.Core.Specifications.ProductSpecifications
{
    public class ProductSpecifications :BaseSpecifications<Product, int>
    {

        // in case hb3t Id 
        public ProductSpecifications(int id) : base(P => P.Id == id) // chain 3la el parameterized const in BaseSpeci to give him id 
        {
           /* Includes.Add(P=>P.Brand)*/;
            //Includes.Add(P=>P.Type);

            ApplyIncludes();
        }



        // In Case Msh Hb3t Id 
        public ProductSpecifications(ProductSpecParams productSpec/*string? sort , int? brandId, int? typeId , int pageSize, int pageIndex*/)  :   base(
            
                 P=> 
                 //lw b null y3ni first part true            ---> msh hakhosh b3d el &&
                 (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
                 &&
                 (!productSpec.BrandId.HasValue   || productSpec.BrandId == P.BrandId)  
                 && 
                 (!productSpec.TypeId.HasValue   || productSpec.TypeId == P.TypeId) )
            
        

        {
            //sort: 
            //name / priceAsc /PriceDesc
           


            //Includes.Add(P => P.Brand);
            //Includes.Add(P => P.Type); 
            ApplyIncludes();



            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":                //Importanttttt
                        AddOrderBy(P => P.Price);  // Set in Prop elly fe BaseSpec BY Fun that i wrththaa "AddOrderBy" -->Then 
                                                   // SpecificationsEvaluator --> hykon 3ndo spec from Ispecification hykon Warth kol el
                                                   // prop doool --> w hytb2 el LINQ  elly htfr2 ben Order asc and desc-->
                                                   // query.OrderBy(spec.OrderBy);   AND    OrderByDescending(spec.OrderByDescending);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }



            //900 products 
            //50 l kol page  --PageSize
            //p3  ---PageIndex 
            //KDA i will Skip 50*(3-1) == 100
            //Skip->  : 100
            //PageIndex :  50 (elly hya pageSize alsn :))


            // da in Skip prop ---------------------------------------------------da in Take prop ---> this is the implementation Of ApplyPagination IN BaseSpecification 
            ApplyPaginations(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);





            }



        



        // H3ml  Fun to Deny Tkrar 
        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }




    }
}
