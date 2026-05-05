namespace API_pro3.Dtos.Products
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
