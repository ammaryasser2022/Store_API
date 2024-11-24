using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface ICasheService
    {
        Task SetCasheKeyAsync(string key, object response, TimeSpan expireTime); // btgeeb data mn Db to make it cashe --> mean make it in inmemoryDb


        Task<string> GetCasheKeyAsync(string key); // btgeeb mn InmemoryDb 
        

    }
}
