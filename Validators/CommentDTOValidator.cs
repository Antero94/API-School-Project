using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class CommentDTOValidator : AbstractValidator<CommentDTO>
{
    public CommentDTOValidator()
    {
        RuleFor(x => x.Content).NotEmpty();
    }
}
