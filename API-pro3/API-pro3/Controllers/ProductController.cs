using API_pro3.Data;
using API_pro3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_pro3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext context) : ControllerBase
    {
        //add crud operations for product
        [HttpGet]
        public IActionResult Get()
        {
            var products = context.Products.ToList();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existingProduct = context.Products.Find(id);
            if (existingProduct == null) return NotFound();
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.UpdateDate = DateTime.Now;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();
            context.Products.Remove(product);
            context.SaveChanges();
            return Ok();
        }

    }
}
