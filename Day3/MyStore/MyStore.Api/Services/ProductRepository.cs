using Microsoft.EntityFrameworkCore;
using MyStore.Api.Contracts;
using MyStore.Api.Data;
using MyStore.Api.Models;

namespace MyStore.Api.Services;


public class ProductRepository(MyStoreDataContext context) : IProductRepository
{
    public async Task<List<Product>> GetAllProducts()
    {
        var result = await context.Products.ToListAsync();
        return result;
    }

    public async Task<Product> AddNewProduct(Product p)
    {
        await context.Products.AddAsync(p);
        await context.SaveChangesAsync();
        return p;
    }

    public async Task<Product?> GetProductById(int id)
    {
        var result = await context.Products.FindAsync(id);
        return result;
    }
}
