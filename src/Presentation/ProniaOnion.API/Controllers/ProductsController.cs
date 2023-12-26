using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Products;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            return Ok(await _service.GetAllPaginated(page,take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreatedDto dto)
        {
            await _service.CreateAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ProductUpdateDto dto)
        {
            if (id <= 0) return BadRequest();
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> SoftDelete(int id)
        //{
        //    await _service.SoftDeleteAsync(id);
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> ReverseSoftDelete(int id)
        //{
        //    await _service.ReverseSoftDeleteAsync(id);
        //    return NoContent();
        //}
    }
}
