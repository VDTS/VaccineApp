using Core.Validators;

using FluentValidation;

namespace Core.Models;
public class ProfileModel
{
    public string? LocalId { get; set; }
    public string? DisplayName { get; set; }
    public string? Role { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? PhotoUrl { get; set; }
    public string? ClusterId { get; set; }
    public string? TeamId { get; set; }
    public string? FamilyId { get; set; }

}
public class ProfileValidator : AbstractValidator<ProfileModel>
{
    public ProfileValidator()
    {
        RuleFor(p => p.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(CommonPropertiesValidator.ValidFullName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
    }
}

