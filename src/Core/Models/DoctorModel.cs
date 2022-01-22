using FluentValidation;

namespace Core.Models;

public class DoctorModel
{
    public string FId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
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
            .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
    }
    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        return name.All(Char.IsLetter);
    }
}
