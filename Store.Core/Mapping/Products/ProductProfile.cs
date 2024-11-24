using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Core.Dtos.Product;
using Store.Core.Entities;

namespace Store.Core.Mapping.Products
{
    public class ProductProfile :Profile
    {
        public ProductProfile(IConfiguration  configuration)
        {
            //CreateMap<Product, ProductDto>();
            // error dont find match between BrandName And Brand(NP)
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(S => S.Brand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(S => S.Type.Name))  // destination to source 
                ////.ForMember(d => d.PictureUrl, options => options.MapFrom(S => S.PictureUrl)); // kda and bgebha mn DB W in DB  NO full Path 
                ////.ForMember(d => d.PictureUrl, options => options.MapFrom(S => $"https://localhost:7144/{S.PictureUrl}")); // kda and bgebha mn DB W in DB  NO full Path 
                //.ForMember(d => d.PictureUrl, options => options.MapFrom(S => $"{configuration["BASEURL"]}{S.PictureUrl}")) // kda and bgebha mn DB W in DB  NO full Path 
                //// the link transferred to appsetting 
                //// will upload the pics on my server in wwwroot 



                //Anthoer Way 
                .ForMember(d => d.PictureUrl, options => options.MapFrom( new PictureUrlResolver(configuration)));
             
            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
        }
    }
}
