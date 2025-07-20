using MyStore.Api.Models;

namespace MyStore.Api.Contracts;

public interface IProductRepository
{
    Task<Product> AddNewProduct(Product p);
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
}
