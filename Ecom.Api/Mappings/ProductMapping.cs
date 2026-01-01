using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().ForMember(x => x.CategoryName, op => op.MapFrom(src => src.Category.Name))
                 .ReverseMap();

            CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<AddProductDto, Product>()
                .ForMember(x => x.Photos, op => op.Ignore()).ReverseMap();
            
            CreateMap<UpdateCategoryDto, Product>()
                .ForMember(x => x.Photos, op => op.Ignore()).ReverseMap();

            
        }
    }
}
