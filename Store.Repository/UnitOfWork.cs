using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;

namespace Store.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;

        private Hashtable _repositories;    // don forget Hash Table Has Key And Value 

        public UnitOfWork(StoreDbContext context )
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> completeAsync()   // make save change so need object from storeDbContext 
        {
            return await _context.SaveChangesAsync();     
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            var type = typeof(TEntity).Name;  // product - brand - type 
            if(!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(type, repository);
            }

            return _repositories[type] as IGenericRepository<TEntity, TKey>;

            //IMPORTANT
            // there is a problem when i need to create object for example from Product in the request then 
            // i need anthor request this function will create new object for me because      who create object -> ME    by new keyword
            // i need clr to make this instead because his k=life time will be scoped 

            // i will make a container carry all object created then compare new object is found in this container or not 
            //  container can be -> HashTable - GUID - Dictionary   -->         private Hashtable _repositories;  





        }
    }
}
