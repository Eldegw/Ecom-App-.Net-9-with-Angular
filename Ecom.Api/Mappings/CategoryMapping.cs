using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

        }
    }
}
