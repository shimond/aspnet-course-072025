using MyStore.Api.Models;
using MyStore.Api.Models.Dtos;

namespace MyStore.Api.Mapping
{
    public class ProductMappingProfile : AutoMapper.Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x=> x.ProductPrice, o=> o.MapFrom(p=>p.Price))
                .ForMember(x => x.ProductName, o => o.MapFrom(p => p.Name)).ReverseMap();


            CreateMap<AddProductRequest, Product>()
                .ForMember(x => x.Price, o => o.MapFrom(p => p.NewProductPrice))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.NewProductName));

        }
    }
}
