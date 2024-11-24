using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos.Basket
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Category { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = " Price Cannot be Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range (1, int.MaxValue , ErrorMessage = " Quantity Must be At least one item")]
        public int Quantity { get; set; }
        
    }
}
