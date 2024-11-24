using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class Product :BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price  { get; set; }

        public ProductBrand Brand { get; set; } // Np

        public int? BrandId { get; set; } //Fk --BY Convention


        public ProductType  Type { get; set; } // Np

        public int? TypeId { get; set; } // Fk --BY Convention

    }
}
