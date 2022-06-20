using CacheWebApi.DataAccess;
using CacheWebApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]s")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_productService.GetById(id));
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        _productService.Add(product);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Product product)
    {
        _productService.Update(product);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(Product product)
    {
        _productService.Delete(product);
        return Ok();
    }
}