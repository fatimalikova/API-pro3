using API_pro3.Models;

namespace API_pro3.Dtos.Categories
{
    public class CategoryReturnDto 
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<ProductInCategoryReturnDto>? Products { get; set; }
    }

    public class ProductInCategoryReturnDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
