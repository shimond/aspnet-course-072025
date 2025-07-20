using FluentValidation;
using FluentValidation.Validators;

namespace MyStore.Api.Validations;

public class ProductValidator : AbstractValidator<MyStore.Api.Models.Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be positive.");

        RuleFor(p => p.Description)
            .Length(5, 100);

        RuleFor(p => p.Price).MustBeEven();
    }
}


public static class RulesExtension
{
    public static IRuleBuilderOptions<T, decimal> MustBeEven<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.Must(value => {
            var clearValue = (int)value;
            return clearValue % 2 == 0;
        })
        .WithMessage("{PropertyName} must be an even number.");
    }
}
