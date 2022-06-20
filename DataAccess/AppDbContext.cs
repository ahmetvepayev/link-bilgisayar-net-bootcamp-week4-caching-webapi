using Microsoft.EntityFrameworkCore;

namespace CacheWebApi.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }


}