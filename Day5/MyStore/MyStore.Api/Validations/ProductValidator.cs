using FluentValidation;
using FluentValidation.Validators;

namespace MyStore.Api.Validations;

public class ProductValidator : AbstractValidator<Models.Dtos.AddProductRequest>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(p => p.ProductPrice)
            .GreaterThan(0)
            .WithMessage("Product price must be positive.");

        RuleFor(p => p.Description)
            .Length(5, 100);

        RuleFor(p => p.ProductPrice).MustBeEven();
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
