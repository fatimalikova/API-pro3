namespace API_pro3.Dtos.Products
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } 
        public int CategoryId { get; set; }
    }
}
