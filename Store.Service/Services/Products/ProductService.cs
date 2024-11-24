using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Core;
using Store.Core.Dtos.Product;
using Store.Core.Entities;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications;
using Store.Core.Specifications.ProductSpecifications;

namespace Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        public async Task</*IEnumerable<ProductDto>*/ PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec/*string? sort ,int? brandId , int? typeId , int? pageSize, int? pageIndex*/)
        {
            // to get all products U need Object from Product Repository --> Unite Of Work who is Responsible to create it so u Need object 
            // from Unite Of Work  


            //return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GettAllAsync());


            //After Specifications


            //var spec = new BaseSpecifications<Product , int >();
            //now:
            var spec =  new ProductSpecifications(productSpec/*sort , brandId ,  typeId , pageSize.Value , pageIndex.Value*/);


            //return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GettAllWithSpecAsync(spec));
            // this use empty onstractor and dont add Includes  


            var products = await _unitOfWork.Repository<Product, int>().GettAllWithSpecAsync(spec);  // Need Mapping From IEnumerable<Product> TOOO IEnumerable<ProductDto>  
            var mappedproducts = _mapper.Map<IEnumerable<ProductDto>>(products);
            //return mappedproducts;
            var countSpec = new ProductWithCountSpecifications(productSpec);
            var Count = await _unitOfWork.Repository<Product, int>().GetCountAsync(countSpec); // but Count msh m7tag kol elly fe spec

            return new PaginationResponse<ProductDto>(productSpec.PageSize, productSpec.PageIndex, Count , mappedproducts);
        }


        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            return _mapper.Map<ProductDto>( await _unitOfWork.Repository<Product,int>().GettWithSpecAsync(spec)); // hna msh hy3ml create l object gden ylaeh in hash table :)
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>> (await _unitOfWork.Repository<ProductBrand, int>().GettAllAsync());
        }

       

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType,int>().GettAllAsync());
        }

       
    }
}
