using API_pro3.Dtos.Categories;
using API_pro3.Models;
using AutoMapper;

namespace API_pro3.Profiles
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<CategoryCreateDto, Category>(); //create
            CreateMap<Category, CategoryReturnDto>(); //read
                //.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Products));
            CreateMap<Product, ProductInCategoryReturnDto>();
        }
    }
}
