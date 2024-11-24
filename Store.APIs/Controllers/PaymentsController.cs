using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Services.Contract;

namespace Store.APIs.Controllers
{
   
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            if (basketId == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var BasketUpdated = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            if (BasketUpdated == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There is A problem With Your Basket"));
            return Ok(BasketUpdated);
        }


        //[HttpPost("webhook")] // http://localhost:7166//api/payments/webhook

    }
}
