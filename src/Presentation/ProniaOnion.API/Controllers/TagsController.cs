using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos.Tags;

namespace ProniaOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {

            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TagCreateDto tagCreateDto)
        {
            await _service.CreateAsync(tagCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] TagUpdateDto dto)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
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

