using Microsoft.EntityFrameworkCore;
using MyStore.Api.Contracts;
using MyStore.Api.Data;
using MyStore.Api.Exceptions;
using MyStore.Api.Models;

namespace MyStore.Api.Services;


public class ProductRepository(MyStoreDataContext context) : IProductRepository
{
    public async Task<List<Product>> GetAllProducts()
    {
        var result = await context.Products
            .AsNoTracking()
            .ToListAsync();
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
        var item = await this.GetProductById(p.Id);
        //p !=  item
        context.Products.Update(p); //  Attach the entity to the context and mark it as modified
        await context.SaveChangesAsync(); // rows affected will be 1 if the entity was found and updated, 0 if it was not found
        return p;

        //save on db without SaveChanged:
        //await context.Products.Where(x=>x.Price < 10)
        //    .ExecuteUpdateAsync(x=> x.SetProperty(y => y.Price, y => y.Price * 1.1m));
    }

    public async Task DeleteProduct(int id)
    {
        // var item = await this.GetProductById(p.Id);
        var rowsCount = await context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
        if(rowsCount == 0)
        {
            throw new ItemNotFoundException($"Product with id {id} not found.", id);
        }   

        //var p = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        //if (p != null)
        //{
        //    context.Products.Remove(p);
        //    await context.SaveChangesAsync();
        //}
        //else
        //{
        //    throw new ArgumentException($"Product with id {id} not found.");
        //}

    }



















}
