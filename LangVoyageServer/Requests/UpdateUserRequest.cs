using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace LangVoyageServer.Requests;

public enum ValidLanguageLevel
{
    A1,
    A2,
    B1,
    B2,
    C1,
    C2
}

public class UpdateUserRequest
{
    public string? Username { get; set; }
    public string? LanguageLevel { get; set; }
}

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.LanguageLevel)
            .Must(BeValidEnum)
            .WithMessage("The provided value is not a valid enum value.");
    }

    private bool BeValidEnum(string? value)
    {
        if (value == null)
            return true;
        return Enum.TryParse(typeof(ValidLanguageLevel), value, true, out _);
    }
}
