using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;

namespace Store.Core.Services.Contract
{
    public interface IPaymentService
    {
        //Create or update payment intent id 

        //  need to have basket to see items and the Amount 
        //  then go to stripe to ask to paymentintentId + Client Secret and return them in basket and return the basket 


        Task<CustomerBasketDto?> CreateOrUpdatePaymentIntentAsync(string BasketId);

        // so go to CustomerBasket and Add paymentintentId +  Client Secret

    }
}
