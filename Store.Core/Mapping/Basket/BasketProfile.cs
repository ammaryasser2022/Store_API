using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;

namespace Store.Core.Mapping.Basket
{
    public class BasketProfile : Profile    
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasketDto,CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto,BasketItem>().ReverseMap();
        }
    }
}
