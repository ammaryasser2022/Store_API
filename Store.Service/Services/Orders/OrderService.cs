using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Entities.OrderEntities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core.Specifications;
using Store.Core.Specifications.orderSpecification;
using Store.Repository;

namespace Store.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork , IBasketRepository basketRepository ,IPaymentService paymentService )
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {

            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;

            var basketItems = new List<OrderItem>();
            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    // need now to convert each item in basket to be a Order item ---> NOTE -> its not mapping those differt types

                    var product = await _unitOfWork.Repository<Product, int>().GettAsync(item.Id);
                    //var productOrderItem = new ProductItemOrder(item.Id, item.ProductName, item.PictureUrl); //first ensure that data in basket == data in DB speicially price
                    var productItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl); //first ensure that data in basket == data in DB speicially price

                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity /* product.Quantity 3ady */);
                    basketItems.Add(orderItem);
                }             
            }

            var subTotal = basketItems.Sum(I => I.Price * I.Quantity);

            var deliverMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GettAsync(deliveryMethodId);


            // If there was order with this PaymentIntentId Remove it ---> Then Create  this Order With new PaymentIntentId
            // if i have PaymentIntentId then i update the order need new one
            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var spec = new OrderSpecificationsWithPaymentIntentId(basket.PaymentIntentId);
                var ExOrder = await _unitOfWork.Repository<Order, int>().GettWithSpecAsync(spec);
                _unitOfWork.Repository<Order,int>().Delete(ExOrder);
            }
            var basketDto = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);


            var order = new Order(buyerEmail, shippingAddress, deliverMethod, basketItems, subTotal, basketDto.PaymentIntentId );
            // Done Creating Order --> Seding To Db
            await _unitOfWork.Repository<Order, int>().AddAsync(order);

            var result = await _unitOfWork.completeAsync();

            if (result <= 0) return null;
            return order;
        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications( buyerEmail,  orderId);
            var order = await _unitOfWork.Repository<Order, int>().GettWithSpecAsync(spec);

            if (order == null) return null;

            return order;

        }

        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, int>().GettAllWithSpecAsync(spec);
            if (orders is null) return null;

            return orders;
            
            
        }
    }
}
