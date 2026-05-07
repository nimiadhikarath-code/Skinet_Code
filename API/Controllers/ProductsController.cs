using System;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interface;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IproductRepository repo):ControllerBase
{
    
 [HttpGet]
 public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts
 (string? brand, string? type,string? sort)
 {
  return Ok( await repo.GetProductsAsync(brand,type,sort));
 }
 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProduct(int id)
 {
  var product = await repo.GetProductByIdAsync(id);
  if (product == null)
  {
   return NotFound();
  }
  return product;
 }
 [HttpPost]
 public async Task<ActionResult<Product>> CreateProduct(Product product)
 {
  repo.AddProduct(product);
  if( await repo.SaveChangesAsync() )
  {
   return CreatedAtAction("GetProduct", new { id = product.Id }, product);    
  }
  return BadRequest("Failed to create product");
 }
 [HttpPut("{id:int}")]
 public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
 {
  if (id != product.Id || !ProductExists(id))
  {
   return BadRequest("Can not update product with id: " + id );
  }
 repo.UpdateProduct(product);
  if( await repo.SaveChangesAsync() )
  {
   return NoContent();    
  }
  return BadRequest("Failed to update product");
 }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        repo.DeleteProduct(product);
       if( await repo.SaveChangesAsync() )
  {
   return NoContent();    
  }
  return BadRequest("Failed to update product");
    }
 private bool ProductExists(int id)
 {
  return repo.ProductExists(id);
 }
}

