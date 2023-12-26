using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Categories;

namespace ProniaOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
                _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {

            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto categoryDto)
        {
            await _service.CreateAsync(categoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id,[FromForm] CategoryUpdateDto dto)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            await _service.UpdateAsync(id, dto);

            return NoContent();
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> SoftDelete(int id)
        //{
        //    if(id<=0) return StatusCode(StatusCodes.Status400BadRequest);
        //    await _service.SoftDeleteAsync(id);
        //    return NoContent();

        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.DeleteAsync(id);
            return NoContent();

        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> ReverseDelete(int id)
        //{
        //    if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
        //    await _service.ReverseSoftDeleteAsync(id);
        //    return NoContent();

        //}
    }
}
