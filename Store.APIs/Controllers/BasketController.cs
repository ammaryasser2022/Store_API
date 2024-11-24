using System.Collections.Specialized;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Repositories;

namespace Store.APIs.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet] //GET : api/basket                   -- Get or Recreate ll basket lw it has been deleted 

        public async Task<ActionResult<CustomerBasket>> GeBasket(string? id)
        {
            // need obj from basket repo

            if (id == null) return BadRequest(new ApiErrorResponse(400 , "Invalid Basket Id"));
            var basket = await  _basketRepository.GetBasketAsync(id);

            // case there is id but no basket // y3ni el basket kant mawgoda bs dead line expired mslan 

            if (basket == null)  new CustomerBasket() { Id =id };  //->  de bd; ma aro7  ll customer basket w a3ml ctor ya5d id 

            return Ok(basket);
        }



        [HttpPost] //POST : api/basket

        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto model )
        {

            // _basketRepository.UpdateBasketAsync(//need map CustomerBasketDto To CustomerBasket  )
            var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model)); // DONT FORGET MAP PROFILE

            if (basket == null) return BadRequest(new ApiErrorResponse(400));
            return Ok(basket);

        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
             await _basketRepository.DeleteBasketAsync(id);
        }

    }
}
