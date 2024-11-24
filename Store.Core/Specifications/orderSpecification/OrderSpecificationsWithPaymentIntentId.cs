using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities.OrderEntities;

namespace Store.Core.Specifications.orderSpecification
{
    public class OrderSpecificationsWithPaymentIntentId :BaseSpecifications<Order , int >
    {
        public OrderSpecificationsWithPaymentIntentId(string PaymentIntentId)
            :base(O=>O.PaymentIntentId == PaymentIntentId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
