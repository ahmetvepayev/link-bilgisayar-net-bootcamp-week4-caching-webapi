using CacheWebApi.DataAccess;

namespace CacheWebApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAll()
    {
        return _productRepository.GetAll();
    }

    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }

    public void Add(Product product)
    {
        _productRepository.Add(product);
    }

    public void Update(Product product)
    {
        _productRepository.Update(product);
    }

    public void Delete(Product product)
    {
        _productRepository.Delete(product);
    }
}