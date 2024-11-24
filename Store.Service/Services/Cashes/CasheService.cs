using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Store.Core.Services.Contract;

namespace Store.Service.Services.Cashes
{
    public class CasheService : ICasheService
    {
        private readonly IDatabase _database;
        public CasheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCasheKeyAsync(string key)
        {
            // to treat with InMemoryDb need Object From IConnectionMultiplexer 

            var casheResponse =  await _database.StringGetAsync(key);
            if (casheResponse.IsNullOrEmpty) return null;    
            
            return casheResponse.ToString();
            
        }

        public async Task SetCasheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return; // mean dont return nothing == STOP THE PROGRAM 

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; 
            // 3shan lma ytl3 json mn inmemroy ytl3o camel case not pascal 

            await _database.StringSetAsync(key, JsonSerializer.Serialize(response , options), expireTime); // lazem a7ol eL OBJECT to Json string 
        }
    }
}
