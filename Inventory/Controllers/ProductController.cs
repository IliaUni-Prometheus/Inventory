using Domain.Models.Abstraction;
using Infrastructure.Models; //TODO:Transfer from Infrastructure to Domain
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// GetProductById
    /// </summary>
    /// <param name="id">The <see cref="int"/></param>
    /// <returns><see cref="{Product}"/></returns>
    [HttpGet]
    public async Task<Product?> GetProductById(int id)
    {
        var product = _repository.GetByIdAsync(id);
        return await product;
    }

    /// <summary>
    /// GetProducts
    /// </summary>
    /// <returns><see cref="IEnumerable{Product}"/></returns>
    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await _repository.GetAllAsync();
        return products;
    }

    /// <summary>
    /// AddProduct
    /// </summary>
    /// <param name="product"><see cref="Product"/></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddProduct([FromBody] Product product)
    {
        var productId = await _repository.InsertAsync(product);
        return productId;
    }

    /// <summary>
    /// DeleteProduct
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete]
    public async Task DeleteProduct(int id)
    {
        await _repository.DeleteAsync(id);
    }
}