using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class PostDTOValidator : AbstractValidator<PostDTO>
{
    public PostDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title kan ikke være tom!")
            .MaximumLength(30).WithMessage("Title kan ikke være lenger enn 30 tegn!");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content kan ikke være tom!");
    }
}
