using System.ComponentModel.DataAnnotations;
using API_pro3.Dtos.Categories;
using FluentValidation;
using PustokApp.Attributes;

namespace API_pro3.Dtos.Categories
{
    public class CategoryCreateDto
    {
       // [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //file qebul etmek ucun IFormFile tipinden bir property elave etmeliyik
        //[FileTypes("image/jpeg", "image/png", "image/gif")] // Yalnız JPEG, PNG ve GIF formatlarını kabul eder
        //[FileLength(2)] // Maksimum dosya boyutu 2 MB
        public IFormFile Image { get; set; } = null!;

    }
}
public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Description is required.");
        RuleFor(c => c.Image)
            .NotNull().WithMessage("Image is required.")
            .Must(file => file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/gif")
            .WithMessage("Only JPEG, PNG, and GIF formats are allowed.")
            .Must(file => file.Length <= 2 * 1024 * 1024) // 2 MB
            .WithMessage("Maximum file size is 2 MB.");
    }
}
