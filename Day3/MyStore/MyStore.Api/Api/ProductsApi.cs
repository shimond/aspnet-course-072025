using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyStore.Api.Contracts;
using MyStore.Api.Models;

namespace MyStore.Api.Api;

public static class ProductsApi
{
    public static IEndpointRouteBuilder MapProductApis(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/products").WithTags("Products");
        group.MapPost("", AddProductAsync);
        group.MapGet("", GetAllProductsAsync);
        
        //group.MapGet("{id}", GetItemById);

        return routes;
    }

    private static async Task<Created<Product>> AddProductAsync(IProductRepository repo, Product product)
    {
        var created = await repo.AddNewProduct(product);
        return TypedResults.Created($"/products/{created.Id}", created);
    }

    private static async Task<Ok<List<Product>>> GetAllProductsAsync(IProductRepository repo)
    {
        var products = await repo.GetAllProducts();
        return TypedResults.Ok(products);
    }
}
