using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyStore.Api.Contracts;
using MyStore.Api.Data;
using MyStore.Api.Exceptions;
using MyStore.Api.Models;

namespace MyStore.Api.Services;


public class ProductRepository(MyStoreDataContext context, ILogger<ProductRepository> logger) : IProductRepository
{
    public async Task<List<Product>> GetAllProducts()
    {
        logger.LogInformation("Retrieving all products from the database");
        var result = await context.Products
            .AsNoTracking()
            .ToListAsync();
        logger.LogInformation("Retrieved {Count} products from the database", result.Count);
        return result;
    }

    public async Task<Product> AddNewProduct(Product p)
    {
        await context.Products.AddAsync(p);
        await context.SaveChangesAsync();
        return p;
    }

    public async Task<Product> GetProductById(int id)
    {
        var result = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p=> p.Id == id);
        if (result != null)
        {
            return result;
        }

        throw new ItemNotFoundException($"Product with id {id} not found.", id);

    }

    public async Task<Product?> UpdateProduct(Product p)
    {
        logger.LogInformation("Updating product with id {Id}", p.Id);
        var item = await this.GetProductById(p.Id);
        context.Products.Update(p); //  Attach the entity to the context and mark it as modified
        await context.SaveChangesAsync(); // rows affected will be 1 if the entity was found and updated, 0 if it was not found
        logger.LogInformation("Product with id {Id} updated successfully", p.Id);
        return p;
    }

    public async Task DeleteProduct(int id)
    {
        logger.LogInformation("Deleting product with id {Id}", id);
        var rowsCount = await context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
        if(rowsCount == 0)
        {
            logger.LogWarning("Product with id {Id} not found for deletion", id);
            throw new ItemNotFoundException($"Product with id {id} not found.", id);
        }   
    }



















}
