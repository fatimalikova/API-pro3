using API_pro3.Data;
using API_pro3.Dtos.Categories;
using API_pro3.Extentions;
using API_pro3.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API_pro3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(AppDbContext context,
        IValidator<CategoryCreateDto> createValidator,
        IMapper mapper) : ControllerBase
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
        public IActionResult Post([FromForm]CategoryCreateDto categoryCreateDto)
        {
            if(!createValidator.Validate(categoryCreateDto).IsValid)
            {
                return BadRequest(createValidator.Validate(categoryCreateDto).Errors.Select(error =>  new
                {
                    error.PropertyName,
                    error.ErrorMessage

                }));
            }
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
        public IActionResult Put([FromRoute]int id, [FromBody]CategoryUpdateDto categoryUpdateDto)
        {
            var existCategory = context.Categories.Find(id);
            if (existCategory == null)return NotFound();
            mapper.Map(categoryUpdateDto, existCategory);
            context.SaveChanges();
            return Ok();
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
