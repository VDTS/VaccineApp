using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class EditProfileModel
{
    public string? LocalId { get; set; }
    public string? DisplayName { get; set; }
    public string? Role { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? PhotoUrl { get; set; }
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}

public class EditProfileModelValidator : AbstractValidator<EditProfileModel>
{
    public EditProfileModelValidator()
    {
        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.Password != x.ConfirmPassword)
            {
                context.AddFailure(nameof(x.Password), "Passwords should match");
            }
        });
    }
}