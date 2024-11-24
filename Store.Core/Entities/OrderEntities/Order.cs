using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.OrderEntities
{
    public class Order :BaseEntity<int>
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public  string BuyerEmail { get; set; }
        public  DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }

        public int DeliveryMethodId { get; set; } //FK
        public  DeliveryMethod  DeliveryMethod { get; set; }

        public  ICollection<OrderItem> Items { get; set; }

        public  decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost; // note this function dont mapped 

        public string PaymentIntentId { get; set; } //  number like receit



    }
}
