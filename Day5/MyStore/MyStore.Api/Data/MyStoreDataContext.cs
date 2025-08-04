using Microsoft.EntityFrameworkCore;
using MyStore.Api.Models;

namespace MyStore.Api.Data;

public class MyStoreDataContext : DbContext
{
    public MyStoreDataContext(DbContextOptions<MyStoreDataContext> options)
        : base(options) // connectionString, DataBaseType, etc.
    {
            
    }
    public DbSet<Product> Products { get; set; }




}
