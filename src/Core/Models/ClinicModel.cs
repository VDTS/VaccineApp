using FluentValidation;

namespace Core.Models;

public class ClinicModel
{
    public Guid Id { get; set; }
    public string FId { get; set; }
    public string ClinicName { get; set; }
    public string Fixed { get; set; }
    public string Outreach { get; set; }
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
            .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
    }
    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        return name.All(Char.IsLetter);
    }
}