using FluentValidation;

namespace Core.Models;
public class ClusterModel
{
    public string? FId { get; set; }
    public Guid Id { get; set; }
    public string? ClusterName { get; set; }
    public ClusterModel()
    {
        Id = Guid.NewGuid();
    }
}
public class ClusterValidator : AbstractValidator<ClusterModel>
{
    public ClusterValidator()
    {
        RuleFor(c => c.ClusterName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty");
    }
}