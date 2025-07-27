using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using MyStore.Api.Contracts;
using MyStore.Api.EndpointFilters;
using MyStore.Api.Models;
using MyStore.Api.Models.Dtos;

namespace MyStore.Api.Api;

public static class ProductsApi
{
    public static IEndpointRouteBuilder MapProductApis(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/products")
            .WithTags("Products");

        group.MapPost("", AddProductAsync);

        //group.MapPost("", AddProductAsync).AddEndpointFilter(async (context, next) => {
        //    foreach (var arg in context.Arguments)
        //    {
        //        if (arg is IValidatable)
        //        {
        //            var argumentType = arg.GetType(); 
        //            var genericType = typeof(IValidator<>).MakeGenericType(argumentType); // open type

        //            var validator = context.HttpContext.RequestServices.GetService(genericType) as IValidator;
        //            if(validator is not  null)
        //            {
        //                var validResult = validator.Validate(new ValidationContext<object>(arg));
        //                if(!validResult.IsValid)
        //                {
        //                    return TypedResults.ValidationProblem(validResult.ToDictionary());
        //                }
        //            }
        //        }
        //    }
        //    var res = await next(context);
        //    return res;

        //}); 

        group.MapGet("", GetAllProductsAsync);
        group.MapPut("/{id}", UpdateProductAsync);
        group.MapDelete("/{id}", DeleteProductAsync);
        group.MapGet("{id}", GetProductById);

        return routes;
    }

    private static async Task<Results<NotFound, Ok<ProductDTO>>> GetProductById(IProductRepository repo, IApplicationMapper mapper, int id)
    {
        var product = await repo.GetProductById(id);
        return TypedResults.Ok(mapper.Map<ProductDTO>(product));
    }


    private static async Task<Results<Created<ProductDTO>, ValidationProblem>> AddProductAsync(
        IProductRepository repo, 
        IApplicationMapper mapper,
        AddProductRequest product)
    {
        var productToInsert = mapper.Map<Product>(product);
        var created = await repo.AddNewProduct(productToInsert);
        ProductDTO result = mapper.Map<ProductDTO>(created);
        Console.WriteLine("In AddProductAsync function 2");
        return TypedResults.Created($"/products/{created.Id}", result);
    }

    private static async Task<Ok<List<ProductDTO>>> GetAllProductsAsync(IProductRepository repo, IApplicationMapper mapper)
    {
        var products = await repo.GetAllProducts();
        var productsDto = mapper.Map<List<ProductDTO>>(products);
        return TypedResults.Ok(productsDto);
    }

    private static async Task<Results<NotFound, Ok<ProductDTO>>> UpdateProductAsync(int id, IProductRepository repo, 
        IApplicationMapper mapper, 
        Product product)
    {
        
        var productToUpdate = await repo.GetProductById(id);
        var updated = await repo.UpdateProduct(product);
        if (updated is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<ProductDTO>(updated));
    }

    private static async Task<Results<NotFound, Ok<ProductDTO>>> DeleteProductAsync(IApplicationMapper mapper, IProductRepository repo, int id)
    {
        var existing = await repo.GetProductById(id);
        await repo.DeleteProduct(id);
        return TypedResults.Ok(mapper.Map<ProductDTO>(existing));
    }
}
