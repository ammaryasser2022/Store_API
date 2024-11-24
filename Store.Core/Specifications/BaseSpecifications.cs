using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Specifications
{
    public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set ; } = new List<Expression<Func<TEntity, object>>> ();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = null;


        //Paginations
        public int Skip { get ; set ; }
        public int Take { get ; set  ; }
        public bool IsPaginationEnabled { get ; set ; }



        // if i left them like this the Criteria , Includes   will be null
        // But I need Criteria = value when Get BY ID && Includes = value When TEntity = Product

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression) // lw 3ndy Criteria ab3tha hna
        {
            Criteria = expression;
           // Includes = new List<Expression<Func<TEntity, object>>>(); // btshwer on list fadya w b4den hb2a amlaha
           // h3mlha default value fo2 // 3shan tkrar


            // in PrductService i have A probkem that was i always call the empty constracor 
            // but i need to add includes 
             // P=>P.brand
             // P=>P.type
            ////// Includes.Add(P=>P.Brand) //Eroor  Brand Not Seen  // just Id And CreateAtt Seen Becauce :BaseEntity 
             //*******SO I wil Generate Class For Each Entity (Product)
        }

        // lw m3ndesh Criteria , Includes
        public BaseSpecifications()
        {
            //Criteria = null; // h3mlha Default value
            //  Includes = new List<Expression<Func<TEntity, object>>>(); 
            // h3mlha default value fo2  // 3shan tkrar

        }


        // M3ak Dymn 2 Options lma n3ml object mn class da --> ya tb3t mn 8er Parametrs y tb3tly Criteria 3la 7sb Your Query



        //Sorting

        public void AddOrderBy(Expression<Func<TEntity, object >> expression)  // 3mlna Set in Prop BY Fun // But With Criteria kan With Ctor
                                                                                    // the Same Idea 
        {
            OrderBy = expression; 
        }
        public void AddOrderByDescending(Expression<Func<TEntity, object >> expression)  // 3mlna Set in Prop BY Fun 
        {
            OrderByDescending = expression; 
        }


        //Paginations

        public void ApplyPaginations(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }






    }
}
