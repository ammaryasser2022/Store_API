using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;

namespace Store.Core
{
    public interface IUnitOfWork
    {
        Task<int> completeAsync();  // Make Save Change One Time --> context.SaveChange  // for tkrar lw b3ml request has Add Update Delete 
                                                                                        //  Dont need to save change many times 








        // Create Repository<T> and return it 
        // mean this fun create a repo from spesific type base on TEntity  then return it 

        IGenericRepository<TEntity , TKey > Repository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
    }
}
