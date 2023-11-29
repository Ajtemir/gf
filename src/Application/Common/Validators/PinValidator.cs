using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Common.Validators;

public partial class PinValidator<T> : PropertyValidator<T, string?>
{
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        if (!value.StartsWith("1") && !value.StartsWith("2"))
        {
            context.AddFailure($"'{context.PropertyName}' должен начинаться с '1' или '2'.");
        }

        if (value.Length != 14)
        {
            context.AddFailure($"'{context.PropertyName}' должен иметь длину 14 цифр. Текущая длина: {value.Length}");
        }

        var result = PinRegex().Match(value);
        if (!result.Success)
        {
            return false;
        }

        return true;
    }

    public override string Name => typeof(PinValidator<>).Name;

    protected override string GetDefaultMessageTemplate(string errorCode) => "'{PropertyName}' является инвалидным.";

    [GeneratedRegex("^(1|2)\\d{13}$")]
    private static partial Regex PinRegex();
}