namespace API_pro3.Dtos.Products
{
    public class ProductReturnDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public CategoryInProductReturnDto Category { get; set; } = null!;
    }

    public class CategoryInProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate{ get; set; }
    }
}
