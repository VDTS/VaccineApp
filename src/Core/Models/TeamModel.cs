using Core.Validators;

using FluentValidation;

namespace Core.Models;
public class TeamModel
{
    public string? FId { get; set; }
    public Guid Id { get; set; }
    public string? TeamNo { get; set; }
    public int SocialMobilizerId { get; set; }
    public string? CHWName { get; set; }
    public TeamModel()
    {
        Id = Guid.NewGuid();
    }
}

public class TeamValidator : AbstractValidator<TeamModel>
{
    public TeamValidator()
    {
        RuleFor(t => t.TeamNo).NotEmpty();
        RuleFor(t => t.CHWName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(CommonPropertiesValidator.ValidFullName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
    }
}