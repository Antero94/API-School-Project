using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username må være med!")
            .MinimumLength(3).WithMessage("Username må være minst 3 tegn!")
            .MaximumLength(30).WithMessage("Username kan ikke være lenger enn 30 tegn!");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname må være med!")
            .MinimumLength(2).WithMessage("Firstname må være minst 2 tegn!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname må være med!")
            .MinimumLength(2).WithMessage("Lastname må være minst 2 tegn!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email må være med!")
            .EmailAddress().WithMessage("Må ha gyldig E-Mail");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password må være med!")
            .MinimumLength(8).WithMessage("Password må være minst 8 tegn!")
            .Matches("[0-9]]").WithMessage("Må ha minst 1 tall i passordet")
            .Matches("[A-Z]").WithMessage("Må ha minst 1 stor bokstav i passordet")
            .Matches("[a-z]").WithMessage("Må ha minst 1 liten bokstav i passordet");
    }
}
