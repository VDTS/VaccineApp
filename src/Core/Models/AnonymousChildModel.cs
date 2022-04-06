using Core.Validators;
using FluentValidation;

namespace Core.Models;

public class AnonymousChildModel
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public DateTime DOB { get; set; }
    public string? Gender { get; set; }
    public string? ChildType { get; set; }
    public bool IsVaccined { get; set; }

    public AnonymousChildModel()
    {
        Id = Guid.NewGuid();
    }
}

public class AnonymousChildModelValidator : AbstractValidator<AnonymousChildModel>
{
    public AnonymousChildModelValidator()
    {
        RuleFor(c => c.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(CommonPropertiesValidator.ValidFullName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
        RuleFor(c => c.Gender).NotEmpty();
        RuleFor(c => c.DOB)
            .Must(DOBValidator.IsChildEligibleForVaccine)
            .WithMessage("Child must be under 5");
    }
}
