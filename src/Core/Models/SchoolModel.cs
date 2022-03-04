using FluentValidation;

namespace Core.Models;

public class SchoolModel
{
    public string FId { get; set; }
    public Guid Id { get; set; }
    public string SchoolName { get; set; }
    public string KeyInfluencer { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public SchoolModel()
    {
        Id = Guid.NewGuid();
    }
}
public class SchoolValidator : AbstractValidator<SchoolModel>
{
    public SchoolValidator()
    {
        RuleFor(s => s.SchoolName)
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
