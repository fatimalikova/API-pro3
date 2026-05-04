using API_pro3.Models;

namespace API_pro3.Dtos.Categories
{
    public class CategoryReturnDto 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<Product> Products { get; set; }
    }
}
