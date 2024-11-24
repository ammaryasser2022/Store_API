using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public string BrandName { get; set; } // Np    // just will be string then ttzbt in Profile config 

        public int? BrandId { get; set; } //Fk --BY Convention 


        public string TypeName   { get; set; } // Np

        public int? TypeId { get; set; } // Fk --BY Convention
    }
}
