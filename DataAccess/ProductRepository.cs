using CacheWebApi.Caching;
using Microsoft.EntityFrameworkCore;

namespace CacheWebApi.DataAccess;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly ICaching _cache;
    private readonly IConfiguration _configuration;
    private readonly CacheOptions _cacheOptions;

    public ProductRepository(AppDbContext context, ICaching cache, IConfiguration configuration)
    {
        _context = context;
        _cache = cache;
        _configuration = configuration;
        _cacheOptions = new CacheOptions();
        _configuration.GetSection("CacheOptions:ProductsList").Bind(_cacheOptions);
    }

    public void UpdateCachedProducts(out List<Product> productList)
    {
        productList = _context.Products.AsNoTracking().ToList();
        _cache.Set(_cacheOptions.Key, productList, _cacheOptions.absoluteLifetime, _cacheOptions.slidingLifetime);
    }

    public void UpdateCachedProducts()
    {
        if (_context.SaveChanges() != 0)
        {
            var productList = _context.Products.AsNoTracking().ToList();
            _cache.Set(_cacheOptions.Key, productList, _cacheOptions.absoluteLifetime, _cacheOptions.slidingLifetime);
        }
    }

    // Only the whole product list is cached
    public List<Product> GetAll()
    {
        if (_cache.TryGet<List<Product>>(_cacheOptions.Key, out List<Product> list))
        {
            return list;
        }

        UpdateCachedProducts(out List<Product> productList);
        return productList;
    }

    // No individual product caching or retrieving
    public Product GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        UpdateCachedProducts();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        UpdateCachedProducts();
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
        UpdateCachedProducts();
    }
}