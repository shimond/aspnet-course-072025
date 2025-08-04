
using FluentValidation;
using MyStore.Api.Models.Dtos;

namespace MyStore.Api.EndpointFilters
{
    public class ValidationEndPointFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            foreach (var arg in context.Arguments)
            {
                if (arg is IValidatable)
                {
                    var argumentType = arg.GetType();
                    var genericType = typeof(IValidator<>).MakeGenericType(argumentType);

                    var validator = context.HttpContext.RequestServices.GetService(genericType) as IValidator;
                    if (validator is not null)
                    {
                        var validResult = validator.Validate(new ValidationContext<object>(arg));
                        if (!validResult.IsValid)
                        {
                            return TypedResults.ValidationProblem(validResult.ToDictionary());
                        }
                    }
                }
            }
            var res = await next(context);
            return res;
        }
    }
}
