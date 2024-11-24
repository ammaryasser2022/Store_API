using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.OrderEntities
{
    public enum OrderStatus
    {
        //NOTE
        // this value will show in Db as 0 , 1 , 2 soi will change it to show with names BY THIS ATTRIBUTE

        [EnumMember(Value = "Pending")] 
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,

        

    }
}
