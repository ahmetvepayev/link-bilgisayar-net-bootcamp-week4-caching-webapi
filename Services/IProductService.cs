using CacheWebApi.DataAccess;

namespace CacheWebApi.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
}