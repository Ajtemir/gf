using System.Linq.Expressions;
using Application.Common.Helpers;
using FluentValidation;

namespace Application.Common.Validators;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> Id<T>(this IRuleBuilder<T, int> ruleBuilder) =>
        ruleBuilder.NotEmpty().GreaterThan(0);

    public static IRuleBuilderOptions<T, int> Page<T>(this IRuleBuilder<T, int> ruleBuilder) =>
        ruleBuilder.GreaterThan(0);

    public static IRuleBuilderOptions<T, int> PageSize<T>(this IRuleBuilder<T, int> ruleBuilder) =>
        ruleBuilder.GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

    public static IRuleBuilderOptions<T, string?> Pin<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder.SetValidator(new PinValidator<T>());

    public static IRuleBuilderOptions<T, string> PassportNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.NotEmpty().MaximumLength(10);

    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.MinimumLength(8).MaximumLength(256);

    public static IRuleBuilderOptions<T, string> UserName<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.MaximumLength(256);

    public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.NotEmpty().MaximumLength(64);

    public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.NotEmpty().MaximumLength(64);

    public static IRuleBuilderOptions<T, string?> PatronymicName<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.MaximumLength(64);

    public static IRuleBuilderOptions<T, string?> Email<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.MaximumLength(64).EmailAddress();

    public static IRuleBuilderOptions<T, string?> Image<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.MaximumLength(FileSizeHelper.ToMegabytes(4));

    public static IRuleBuilderOptions<T, string> RequiredName<T>(this IRuleBuilder<T, string> ruleBuilder,
        int length = 256) =>
        ruleBuilder.NotEmpty().MaximumLength(length);

    public static IRuleBuilderOptions<T, string?> OptionalName<T>(this IRuleBuilder<T, string?> ruleBuilder,
        int length = 256) =>
        ruleBuilder.MaximumLength(length);

    public static IRuleBuilderOptions<T, string?> RequiredNameWhenFieldNotNull<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        Expression<Func<T, string?>> expression,
        int length = 256) =>
        ruleBuilder.MaximumLength(length)
            .NotEmpty()
            .When(e => !string.IsNullOrEmpty(expression.Compile().Invoke(e)));
}