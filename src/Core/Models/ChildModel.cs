using Core.Validators;
using FluentValidation;

namespace Core.Models;
public class ChildModel
{
    public string? FId { get; set; }
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public DateTime DOB { get; set; }
    public string? Gender { get; set; }
    public bool OPV0 { get; set; }
    public int RINo { get; set; }
    public ChildModel()
    {
        Id = Guid.NewGuid();
    }
}

public class ChildValidator : AbstractValidator<ChildModel>
{
    public ChildValidator()
    {
        RuleFor(c => c.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(CommonPropertiesValidator.ValidFullName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
        RuleFor(c => c.Gender).NotEmpty();
        RuleFor(c => c.RINo).NotEmpty();
        RuleFor(c => c.DOB)
            .Must(DOBValidator.IsChildEligibleForVaccine)
            .WithMessage("Child must be under 5");
    }
}