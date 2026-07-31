using Microsoft.AspNetCore.Mvc;
using StajTakip.Core.Dto;
using StajTakip.Service.Services;

namespace StajTakipp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _service;

        public MentorController(IMentorService service)
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
        public async Task<IActionResult> Add(MentorRequest request)
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MentorRequest request)
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
                return BadRequest(new { message = "Bu mentöre bağlı stajyer(ler) olduğu için mentör silinemez. Önce stajyerlerin mentörünü değiştirin veya stajyerleri silin." });
            }

            return NoContent();
        }
    }
}
