using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities.OrderEntities;

namespace Store.Core.Specifications.orderSpecification
{
    public class OrderSpecifications :BaseSpecifications<Order , int >
    {
        public OrderSpecifications(string buyerEmail, int orderId) 
            : base(O => O.BuyerEmail == buyerEmail && O.Id == orderId) // chain 3la el parameterized const in BaseSpeci to give him id 
        {
            ApplyIncludes();
        }
        public OrderSpecifications(string buyerEmail)
             : base(O => O.BuyerEmail == buyerEmail)
        {
            ApplyIncludes();
        }
     

        private void ApplyIncludes()
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
