using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Account;

namespace ProniaOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public UsersController(IAuthenticationService service)
        {
            _service = service;
        }
        //[HttpPost]
        //public async Task<IActionResult> Register([FromForm]RegisterDto dto)
        //{
        //    await _service.Register(dto);
        //    return NoContent();
        //}
        [HttpPost/*("{login}")*/]
        public async Task<IActionResult> Login([FromForm]LoginDto dto)
        {
           
            string token = await _service.Login(dto);
            return Ok(token);
        }
    }
}
