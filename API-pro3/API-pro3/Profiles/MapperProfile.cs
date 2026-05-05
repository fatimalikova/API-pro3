using API_pro3.Dtos.Categories;
using API_pro3.Dtos.Products;
using API_pro3.Extentions;
using API_pro3.Models;
using AutoMapper;

namespace API_pro3.Profiles
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.SaveFile("wwwroot/images/")));
            CreateMap<Category, CategoryReturnDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => "http://localhost:5132/images/" + src.ImageUrl ));
            CreateMap<CategoryUpdateDto, Category>(); //update
            CreateMap<Product, ProductInCategoryReturnDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReturnDto>();
            CreateMap<Category, CategoryInProductReturnDto>();
        }
    }
}
