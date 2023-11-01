using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Models.DTOs;

//public record UserRegistrationDTO1(
//    string UserName,
//    string FirstName,
//    string LastName,
//    string Password,
//    string Email);

public class UserRegistrationDTO
{
    //[Required(ErrorMessage = "Username må være med!")]
    //[MinLength(3, ErrorMessage = "Username må være minst 3 tegn!")]
    //[MaxLength(30, ErrorMessage = "Username kan ikke være lenger enn 30 tegn!")]

    public string UserName { get; init; } = string.Empty;

    //[Required(ErrorMessage = "Firstname må være med!")]
    public string FirstName { get; init; } = string.Empty;

    //[Required(ErrorMessage = "Lastname må være med!")]
    public string LastName { get; init; } = string.Empty;

    //[Required(ErrorMessage = "Passord må være med!")]
    public string Password { get; init; } = string.Empty;

    //[Required(ErrorMessage = "Email må være med!")]
    //[EmailAddress(ErrorMessage = "Må ha gyldig email!")]
    public string Email { get; init; } = string.Empty;
}