using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Dtos.Auth;
using Store.Core.Entities.OrderEntities;

namespace Store.Core.Dtos.Orders
{
    public class OrderDto
    {

        [Required(ErrorMessage = "Basket ID is Required :(")]

        public string  BasketId { get; set; }


        [Required(ErrorMessage = "DeliveryMethod ID is Required :(")]
        public int DeliveryMethodId { get; set; } 


        public AddressDto ShipToAddress { get; set; }


    }
}
