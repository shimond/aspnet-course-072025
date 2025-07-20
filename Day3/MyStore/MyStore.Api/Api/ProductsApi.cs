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

    private static async Task<Results<NotFound, Ok<Product>>> GetProductById(IProductRepository repo, int id)
    {
        var product = await repo.GetProductById(id);
        return TypedResults.Ok(product);
    }


    private static async Task<Results<Created<ProductDTO>, ValidationProblem>> AddProductAsync(
        IProductRepository repo, 
        IValidator<AddProductRequest> validator,
        AddProductRequest product)
    {

        var validationResults = await validator.ValidateAsync(product);
        if (!validationResults.IsValid)
        {
            return TypedResults.ValidationProblem(validationResults.ToDictionary());
        }

        var productToInsert = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
        };

        var created = await repo.AddNewProduct(productToInsert);
        ProductDTO result = new ProductDTO
        {
            Id = created.Id,
            ProductName = created.Name,
            Description = created.Description,
            ProductPrice = created.Price
        };
        return TypedResults.Created($"/products/{created.Id}", result); 
    }

    private static async Task<Ok<List<Product>>> GetAllProductsAsync(IProductRepository repo)
    {
        var products = await repo.GetAllProducts();
        return TypedResults.Ok(products);
    }

    private static async Task<Results<NotFound, Ok<Product>>> UpdateProductAsync(int id, IProductRepository repo, Product product)
    {
        
        var productToUpdate = await repo.GetProductById(id);
        var updated = await repo.UpdateProduct(product);
        if (updated is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(updated);
    }

    private static async Task<Results<NotFound, Ok<Product>>> DeleteProductAsync(int id, IProductRepository repo)
    {
        var existing = await repo.GetProductById(id);
        await repo.DeleteProduct(id);
        return TypedResults.Ok(existing);
    }
}
