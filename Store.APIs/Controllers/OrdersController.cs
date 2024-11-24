using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;
using Store.Core;
using Store.Core.Dtos.Orders;
using Store.Core.Entities.OrderEntities;
using Store.Core.Services.Contract;

namespace Store.APIs.Controllers
{

    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService, IMapper mapper , IUnitOfWork unitOfWork) 
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpPost] // Baseurl/api/orders
        public async Task<ActionResult<Order>> CreateOrder(/*string basketId*/ OrderDto model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var address = _mapper.Map<Address>(model.ShipToAddress);


            var order = await _orderService.CreateOrderAsync(userEmail, model.BasketId, model.DeliveryMethodId, address);
            if (order == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));


            return Ok(_mapper.Map<OrderToReturnDto>(order));

        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetOrdersForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var orders = await _orderService.GetOrdersForSpecificUserAsync(userEmail);
            if (orders == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var returnedOrders = _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
            return Ok(returnedOrders);
        }




        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOrderByIdForSpecificUser(int Id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));


            var order = await _orderService.GetOrderByIdForSpecificUserAsync(userEmail , Id );
            if (order == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var returnedOrder = _mapper.Map<OrderToReturnDto>(order);
            return Ok(returnedOrder);
        }



        [HttpGet("DeliveryMehods")]
        public async Task<ActionResult> GetAllDeliveryMethods()
        {
            var deliiveryMethods =  await _unitOfWork.Repository<DeliveryMethod,int>().GettAllAsync();
            if (deliiveryMethods == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));


            return Ok(deliiveryMethods);
        }
    }
}
