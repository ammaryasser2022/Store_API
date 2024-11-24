using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;
using Store.Core.Specifications;

namespace Store.Core.Repositories.Contract
{
    public interface IGenericRepository< TEntity , TKey > where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GettAllAsync();
        Task<TEntity>GettAsync(TKey id);

        //-------------
        // For lw Hsht8l By Specifications
        Task<IEnumerable<TEntity>> GettAllWithSpecAsync(ISpecifications<TEntity, TKey > spec );
        Task<TEntity> GettWithSpecAsync( ISpecifications<TEntity, TKey> spec);


        // ------------

        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);

        Task AddAsync(TEntity entity);

        
        void Update(TEntity entity);     // msh btsht8l Async !!!
        void Delete(TEntity entity);     // msh btsht8l Async !!!


    }
}
