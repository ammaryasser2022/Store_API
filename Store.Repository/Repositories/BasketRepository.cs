using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;

namespace Store.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis ) 
        {

            _database = redis.GetDatabase();
        }


        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            //return basket; //  data btrgy -> as RedisValue ==jsonstring   and i need it as CustomerBasket
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);    
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            

            var createdOrUpdatedBasket = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if(createdOrUpdatedBasket is false) { return null; } // lw m3mltsh creat rg3 NULL
            return await GetBasketAsync(basket.Id); //lw 3mlt Rgy el basket elly b this id  
        }
    }
}
 