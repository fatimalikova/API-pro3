using API_pro3.Data;
using API_pro3.Dtos.Products;
using API_pro3.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_pro3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            var productReturnDto = mapper.Map<ProductReturnDto>(product);
            return Ok(productReturnDto);
        }

        //add crud operations for product
        [HttpGet]
        public IActionResult Get()
        {
            var products = context.Products.Include(p => p.Category).ToList();
            var productreturnDtos = mapper.Map<List<ProductReturnDto>>(products);
            return Ok(productreturnDtos);
        }


        [HttpPost]
        public IActionResult Add(ProductCreateDto productCreateDto)
        {
            var category = context.Categories.Find(productCreateDto.CategoryId);
            if(category == null) return BadRequest("Invalid category id");
            var product = mapper.Map<Product>(productCreateDto);
            context.Products.Add(product);
            context.SaveChanges();
            return Ok();

        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto productUpdateDto)
        {
            var existingProduct = context.Products.Find(id);
            if (existingProduct == null) return NotFound();
            mapper.Map(productUpdateDto, existingProduct);
            existingProduct.CategoryId = productUpdateDto.CategoryId;
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
