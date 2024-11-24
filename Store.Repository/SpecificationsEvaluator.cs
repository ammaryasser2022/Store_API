using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Specifications;

namespace Store.Repository
{
    // Class responsible for have Functions To Generate Query 

    internal static class SpecificationsEvaluator<TEntity ,TKey> where TEntity : BaseEntity<TKey>
    {
        //Creat And Query 
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)  // awel 7aga in  Query _context.Products --> is a IQuarable
        {
            var query = inputQuery; // if Query Dont Need ISpecifications

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // el where bt5d value as expression<Func...... this is in Criteria

            }
            // and evey include to query if U have Includes 


            //Sorting

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if(spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }



            //_context.Products =                                                         , 1->P=>P.Brands  2->P=>P.types            
            query = spec.Includes.Aggregate(query , (currentQuery /*1-> _context.Products*/, includeExpressions)         =>     currentQuery.Include(includeExpressions)); // 1-> _context.Products/Include(P=>P.Brands)
            return query;                                        // 2-> _context.Products/Include(P=>P.Brands)                                                             // 2-> _context.Products/Include(P=>P.Brands).Includes(P=>P.types)   



        }

    }   
}
