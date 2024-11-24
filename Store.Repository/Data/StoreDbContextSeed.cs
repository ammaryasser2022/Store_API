using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Store.Core.Entities;
using Store.Core.Entities.OrderEntities;
using Store.Repository.Data.Contexts;

namespace Store.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync( StoreDbContext _context )
        {
          if( _context.Brands.Count() == 0 ) // Make Sure the Data is not entered asln 
            {
                //Brand
                //1 Read Data From Json
                // but first upload Data Files in the Project --> Data/ DataSeed --> BY Drag Drop 

                var brandsData = await File.ReadAllTextAsync(@"..\Store.Repository\Data\DataSeed\brands.json"); //current dir in Store.APIs --> ..\ brg3 step behind  
                                                                                                     // path --> D:\Route\Web APIs\Store\Store.Repository\Data\DataSeed\brands.json  


                //2.  need to convert brandsData from Jsonstring to list of ProductBrand
                var brands =  JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


                //3. Add/Seed Data  To Db 
                // so need object from StoreDbContext  

                //add range because need to add list 
                if (brands != null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }

            }


          if (_context.Types.Count() == 0)
            {
                //1. Upload Data in my Project --Done In last Step 
                //1. Read Data 
                var typesData = await File.ReadAllTextAsync(@"..\Store.Repository\Data\DataSeed\types.json");

                //2. Convert Type

                var types =  JsonSerializer.Deserialize<List<ProductType>>(typesData);

                //3. Seed Data 
                if (types is not null && types.Count()>0 )
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();

                }



            }


          if (_context.Products.Count()==0)
            {
                var productData = await File.ReadAllTextAsync(@"..\Store.Repository\Data\DataSeed\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }


          if (_context.DeliveryMethods.Count() == 0)
           {
               var deliveryData = await File.ReadAllTextAsync(@"..\Store.Repository\Data\DataSeed\delivery.json");
           
               var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
           
               if (deliveryMethods is not null && deliveryMethods.Count() > 0)
               {
                   await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                   await _context.SaveChangesAsync();
               }
           }
        }


        
    }
}
