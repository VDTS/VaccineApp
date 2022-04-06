using Core.Validators;

using FluentValidation;

namespace Core.Models;

public class ClinicModel
{
    public Guid Id { get; set; }
    public string? FId { get; set; }
    public string? ClinicName { get; set; }
    public string? Fixed { get; set; }
    public string? Outreach { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public ClinicModel()
    {
        Id = Guid.NewGuid();
    }
}

public class ClinicValidator : AbstractValidator<ClinicModel>
{
    public ClinicValidator()
    {
        RuleFor(c => c.ClinicName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(CommonPropertiesValidator.ValidFullName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
    }
}