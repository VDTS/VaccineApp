using FluentValidation;
using Newtonsoft.Json;

namespace Core.Models;
public class SignInModel
{
    [JsonProperty("email")]
    public string? Email { get; set; }
    [JsonProperty("password")]
    public string? Password { get; set; }
    [JsonProperty("returnSecureToken")]
    public bool ReturnSecureToken { get; set; }
}

public class SignInModelValidator : AbstractValidator<SignInModel>
{
    public SignInModelValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(a => a.Password)
            .NotEmpty()
            .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
            .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
    }
}
