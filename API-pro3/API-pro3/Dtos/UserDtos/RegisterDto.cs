using API_pro3.Dtos.UserDtos;
using FluentValidation;

namespace API_pro3.Dtos.UserDtos
{
    public class RegisterDto
    {
        public string Fullname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}

public class  RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(r => r.Fullname)
            .NotEmpty().WithMessage("Fullname is required.")
            .MaximumLength(100).WithMessage("Fullname cannot exceed 100 characters.");
        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(r => r.ConfirmPassword)
            .Equal(r => r.Password).WithMessage("Passwords do not match.");
    }

}
