using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.ProductSpecifications
{
    public class ProductSpecParams
    {
        private string? search;
                                        // de full prop because i need to change the value of search to lowerr 
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }



        public  string? Sort { get; set; }
        public  int? BrandId { get; set; }
        public  int? TypeId { get; set; }
        public int PageSize { get; set; } = 5;

        //Or                                       // if value Akbr mn 10 htb2a 10 3afya else 7tt a2l bra7tkk
        //private int pageSize = 5 ;  //-> LW MB3TSH 5lesh 5    /// bdl ma a3lhom nullable 3shan lw nullabe w products kteer hy3l overload  

        //public int PageSize
        //{
        //    get { return pageSize; }
        //    set { pageSize = value > 10 ? 10 : value; }
        //}

        public int PageIndex { get; set; } = 1;
        
        // No oRR
    }
}
