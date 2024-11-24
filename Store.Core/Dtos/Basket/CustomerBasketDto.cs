using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Dtos.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }


        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; } // who will pay 
    }
}
