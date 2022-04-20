using AutoMapper;
using Northwind.Entities.Models;
using Northwind.Entities.DTO;

namespace NorthwinWebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
           
            CreateMap<Customer, CustomerDto>().ReverseMap();
            //CreateMap<CustomerDto, Customer>();
            CreateMap<ProductDto, Product>().ReverseMap();
            //CreateMap<Product, ProductDto>();
        }
    }
}
