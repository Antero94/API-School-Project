﻿using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDTOValidator()
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

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Passord må være med!")
            .MinimumLength(8).WithMessage("Passord må være minst 8 tegn!")
            .MaximumLength(50).WithMessage("Passord kan ikke være lenger enn 50 tegn!")
            .Matches("[0-9]").WithMessage("Må ha minst 1 tall i passordet!")
            .Matches("[A-Z]").WithMessage("Må ha minst 1 stor bokstav i passordet!")
            .Matches("[a-z]").WithMessage("Må ha minst 1 liten bokstav i passordet!");
    }
}
