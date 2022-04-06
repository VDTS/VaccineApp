using Core.Validators;

using FluentValidation;

namespace Core.Models;

public class DoctorModel
{
    public string? FId { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsHeProvindingSupportForSIAAndVaccination { get; set; }
    public DoctorModel()
    {
        Id = Guid.NewGuid();
    }
}
public class DoctorValidator : AbstractValidator<DoctorModel>
{
    public DoctorValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(CommonPropertiesValidator.ValidFullName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
    }
}
