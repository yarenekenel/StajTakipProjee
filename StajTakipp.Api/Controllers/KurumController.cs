using Microsoft.AspNetCore.Mvc;
using StajTakip.Core.Dto;
using StajTakip.Service.Services;

namespace StajTakipp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KurumController : ControllerBase
    {
        private readonly IKurumService _service;

        public KurumController(IKurumService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(KurumRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KurumRequest request)
        {
            await _service.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var basarili = await _service.DeleteAsync(id);

            if (!basarili)
            {
                return BadRequest(new { message = "Bu kuruma bağlı stajyer(ler) olduğu için kurum silinemez. Önce stajyerlerin kurumunu değiştirin veya stajyerleri silin." });
            }

            return NoContent();
        }
    }
}