using Microsoft.AspNetCore.Mvc;
using TASK_session5.Data;
using TASK_session5.Data.models;
using TASK_session5.DTOs;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TASK_session5.Migrations;
using Mapster;

namespace TASK_session5.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var product = await context.products.Select(
                  x => new GetProductDtos()
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Price = x.Price,
                      Description = x.Description
                  }
                  ).ToListAsync();
            return Ok(product);
        }
            [HttpGet("Create")]
        public async Task<IActionResult> CreateProductAsync( CreateProductDots prod, [FromServices] IValidator<CreateProductDots> validator)
        {
            var result = validator.Validate(prod);
            if (!result.IsValid)
            {
                var modelState = new ModelStateDictionary();
               result.Errors.ForEach(err => modelState.AddModelError(err.PropertyName, err.ErrorMessage));
                return ValidationProblem();
            }
            var product = prod.Adapt<Product>();
           
            await context.products.AddAsync(product);
            await context.SaveChangesAsync();
            return Ok(product);




        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProductAsync(int id, CreateProductDots prod)
        {
            var product = await context.products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Description = prod.Description;
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await context.products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            context.products.Remove(product);
            await context.SaveChangesAsync();
            return Ok($"Delete employee with id {id}");
        }
    }
}
