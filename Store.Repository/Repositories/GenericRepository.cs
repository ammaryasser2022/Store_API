using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Specifications;
using Store.Repository.Data.Contexts;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GettAllAsync()
        {
            //need object from storeDbContext

            if (typeof(TEntity) == typeof(Product))   //--- it wrong we will ise design pattern for includes but its temporaryyy
            {
                return (IEnumerable<TEntity>)await _context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync();
            }  // fe design Pattern to avoid this in next session  /// Important 

            return await _context.Set<TEntity>().ToListAsync();
        }


        public async  Task<TEntity> GettAsync(TKey id) //--- it wrong we will ise design pattern for where  but its temporaryyy
        {
            if (typeof(TEntity) == typeof(Product))
            {
                //return  await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(p=>p.Id == id  as int? ) as TEntity;
                return await _context.Products.Include(P => P.Brand).Where(p => p.Id == id as int?).Include(P => P.Type).FirstOrDefaultAsync() as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);  
            // here i dont make Save Changes !!! --> apply in Unite Of wORK
        }

       
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GettAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
           //return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).ToListAsync();
           return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity> GettWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            //return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).FirstOrDefaultAsync();
            return await ApplySpecification(spec).FirstOrDefaultAsync();

        }


        //For Tkrar 
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec); 
        }

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).CountAsync(); 
        }
    }
}
