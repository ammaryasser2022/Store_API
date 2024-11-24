using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Specifications
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>  
    {

        ////  return  await _context.Products.Include(P => P.Brand).Where(p => p.Id == id as int?).Include(P => P.Type).FirstOrDefaultAsync() as TEntity;


        // for where --> where bta5d Expressions of func of one parameter (TEntity)   && Return BOOL
        public Expression<Func<TEntity , bool >> Criteria { get; set; }  // Criteria mean m3yyer or mwasfat --> mean i will get data with this m3yyer

        


        //// return (IEnumerable<TEntity>) await _context.Products.Include(P=>P.Brand).Include(P=>P.Type).ToListAsync();

        public List<Expression<Func<TEntity , object>>> Includes { get; set; }



        public Expression<Func<TEntity,object >> OrderBy { get; set; }
        public Expression<Func<TEntity,object >> OrderByDescending   { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
