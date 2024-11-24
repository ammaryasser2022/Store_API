using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Core;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Entities.OrderEntities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Stripe;
using Stripe.V2;
using Product = Store.Core.Entities.Product;

namespace Store.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration ,IBasketRepository basketRepository , IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<CustomerBasketDto?> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            // bklm stripe by secrit key 
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"]; 

            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket == null) return null;

            //Amount == Subtotal + delivery method cost 
            var ShippingPrice = 0.0M; 
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliverMethod = await _unitOfWork.Repository<DeliveryMethod , int>().GettAsync(Basket.DeliveryMethodId.Value); //.Value because it Nullable 
                ShippingPrice = DeliverMethod.Cost;

            }

            // check all Prices is equal to Db or not 

            if (Basket.Items.Count > 0)
            {

                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product , int>().GettAsync(item.Id);
                    if (item.Price != Product.Price)
                    {
                        item.Price = Product.Price;
                    }
                }
            }

            var SubTotal =  Basket.Items.Sum(I=>I.Price * I.Quantity);

            var Amount = ShippingPrice + SubTotal;




            // Need To Create PaymentIntentId ---> Need To User Service that Already EXIST with Package 

            
            var Service = new PaymentIntentService();
            // with this onbject now i can create  and update payment intent id 
            var PaymentIntent = new PaymentIntent(); // skip for now will know farther 

            if (string.IsNullOrEmpty(Basket.PaymentIntentId)) //Create 
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Amount *100 , 
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}
                    
                };

                PaymentIntent = await Service.CreateAsync(Options); // will Return PaymentIntent ( PaymentIntentId And ClientSecrit )

                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;

            }
            else //Update 
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Amount * 100
                };

                PaymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, Options);
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }

            await _basketRepository.UpdateBasketAsync(Basket); //// ****Dont Forget 

            var BasketReturned = _mapper.Map<CustomerBasketDto>(Basket);


            return BasketReturned;


         }
    }
}
