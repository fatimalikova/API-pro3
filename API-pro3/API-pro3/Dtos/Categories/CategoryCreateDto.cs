using PustokApp.Attributes;

namespace API_pro3.Dtos.Categories
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //file qebul etmek ucun IFormFile tipinden bir property elave etmeliyik
        //[FileTypes("image/jpeg", "image/png", "image/gif")] // Yalnız JPEG, PNG ve GIF formatlarını kabul eder
        //[FileLength(2)] // Maksimum dosya boyutu 2 MB
        public IFormFile Image { get; set; } = null!;

    }
}
