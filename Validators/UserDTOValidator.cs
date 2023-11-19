using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username må være med!")
            .MinimumLength(3).WithMessage("Username må være minst 3 tegn!")
            .MaximumLength(30).WithMessage("Username kan ikke være lenger enn 30 tegn!")
            .Matches("^[a-zA-Z]").WithMessage("Username må begynne med en bokstav!")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Username kan bare inneholde tall og bokstaver!");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname må være med!")
            .MinimumLength(2).WithMessage("Firstname må være minst 2 tegn!")
            .MaximumLength(20).WithMessage("Firstname kan ikke være lenger enn 20 tegn!")
            .Matches("^[a-zA-Z]+$").WithMessage("Firstname kan bare inneholde bokstaver!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname må være med!")
            .MinimumLength(2).WithMessage("Lastname må være minst 2 tegn!")
            .MaximumLength(20).WithMessage("Lastname kan ikke være lenger enn 20 tegn!")
            .Matches("^[a-zA-Z]+$").WithMessage("Lastname kan bare inneholde bokstaver!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail må være med!")
            .EmailAddress().WithMessage("Må ha gyldig E-Mail");
    }
}
