using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using MyStore.Api.Contracts;
using MyStore.Api.Models;
using MyStore.Api.Models.Dtos;

namespace MyStore.Api.Api;

public static class ProductsApi
{
    public static IEndpointRouteBuilder MapProductApis(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/products").WithTags("Products");
        group.MapPost("", AddProductAsync); 
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
        IValidator<AddProductRequest> validator,
        IApplicationMapper mapper,
        AddProductRequest product)
    {

        var validationResults = await validator.ValidateAsync(product);
        if (!validationResults.IsValid)
        {
            return TypedResults.ValidationProblem(validationResults.ToDictionary());
        }

        var productToInsert = mapper.Map<Product>(product);
        var created = await repo.AddNewProduct(productToInsert);
        ProductDTO result = mapper.Map<ProductDTO>(created);
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
