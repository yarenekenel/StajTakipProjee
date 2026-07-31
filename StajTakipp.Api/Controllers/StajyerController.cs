using Microsoft.AspNetCore.Mvc;
using StajTakip.Core.Dto;
using StajTakip.Service;
using System.Reflection.Metadata.Ecma335;

namespace StajTakipp.Api.Controllers
{
    //dışarıdan (Swagger'dan/tarayıcıdan) gelen istekleri karşılayacak Controller'ı yazacağız.
    //Bu 5 metot, ileride formun "Kaydet", "Listele", "Güncelle", "Sil" butonlarının bağlanacağı yerler olacak.

    [ApiController]
    [Route("api/[controller]")]
    public class StajyerController : ControllerBase
    {
        private readonly IStajyerService _service;

        public StajyerController(IStajyerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
        var result = await _service.GetAllAsync();
        return Ok(result);
        }

        [HttpGet("detayli")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            var result = await _service.GetAllWithDetailsAsync();
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
        public async Task<IActionResult> Add(StajyerRequest request)
        {
            try
            {
                var result = await _service.AddAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }


    }
}
