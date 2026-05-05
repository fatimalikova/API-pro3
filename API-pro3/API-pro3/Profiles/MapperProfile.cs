using API_pro3.Dtos.Categories;
using API_pro3.Dtos.Products;
using API_pro3.Extentions;
using API_pro3.Models;
using AutoMapper;

namespace API_pro3.Profiles
{
    public class MapperProfile :Profile
    {
        public MapperProfile(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var uriBuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme, // "http" or "https"
                Host = httpContext.Request.Host.Host, // "localhost" or your domain
                Port = httpContext.Request.Host.Port ?? 80 // Default to port 80 if not specified
            };
            var url = uriBuilder.Uri.AbsoluteUri;



            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.SaveFile("wwwroot/images/")));
            CreateMap<Category, CategoryReturnDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => url + "images/" + src.ImageUrl ));
            CreateMap<CategoryUpdateDto, Category>(); //update
            CreateMap<Product, ProductInCategoryReturnDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReturnDto>();
            CreateMap<Category, CategoryInProductReturnDto>();
        }
    }
}
