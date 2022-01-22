using FluentValidation;

namespace Core.Models;

public class InfluencerModel
{
    public string FId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Contact { get; set; }
    public bool DoesHeProvidingSupport { get; set; }
    public InfluencerModel()
    {
        Id = Guid.NewGuid();
    }
}
public class InfluencerValidator : AbstractValidator<InfluencerModel>
{
    public InfluencerValidator()
    {
        RuleFor(i => i.Name)
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