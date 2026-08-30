using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly StoreContext _storeContext;
    public ProductController(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _storeContext.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _storeContext.Products.FindAsync(id);

        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product){
        _storeContext.Products.Add(product);
        await _storeContext.SaveChangesAsync();
        return product;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product){
        if (product.Id != id || !ProductExists(id)) return BadRequest();
        _storeContext.Entry(product).State = EntityState.Modified;
        await _storeContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id){
        var product = await _storeContext.Products.FindAsync(id);
        if (product == null) return NotFound();
        _storeContext.Products.Remove(product);
        await _storeContext.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _storeContext.Products.Any(p => p.Id == id);
    }
}