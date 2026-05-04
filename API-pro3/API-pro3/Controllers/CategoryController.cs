using API_pro3.Data;
using API_pro3.Dtos.Categories;
using API_pro3.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_pro3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return StatusCode(StatusCodes.Status200OK, "Get all categories");
                  // ===
            // return Ok("Get all categories");

            var categories = context.Categories
                .Include(c => c.Products)
                .ToList();

            var categoryDtos = mapper.Map<List<CategoryReturnDto>>(categories);
            return Ok(categoryDtos);
        }


        [HttpGet("{id}")] // category/?id=1  -> category/1
        //feillerden istifade etmemeye calish
        public IActionResult Get(int id)
        {
            var category = context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = mapper.Map<CategoryReturnDto>(category);
            return Ok(categoryDto);
        }
        [HttpPost]
        public IActionResult Post(CategoryCreateDto categoryCreateDto)
        {
            //var newCategory = new Category
            //{
            //    Name = categoryCreateDto.Name,
            //    Description = categoryCreateDto.Description,
            //    CreateDate = DateTime.Now
            //};
            var newCategory = mapper.Map<Category>(categoryCreateDto);
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Category category)
        {
           var existCategory = context.Categories.Find(id);
            if (existCategory == null)
            {
                return NotFound();
            }
            existCategory.Name = category.Name;
            existCategory.Description = category.Description;
            existCategory.UpdateDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return NoContent();
        }
    }
}
