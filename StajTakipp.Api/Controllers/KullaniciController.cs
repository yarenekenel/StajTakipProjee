using Microsoft.AspNetCore.Mvc;
using StajTakip.Service.Services;
using StajTakip.Core.Dto;

namespace StajTakipp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _service;

        public KullaniciController(IKullaniciService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _service.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı." });
            }

            return Ok(result);
        }

        [HttpPost("kullanici-olustur-gecici")]
        public async Task<IActionResult> KullaniciOlusturGecici(LoginRequest request)
        {
            await _service.RegisterAsync(request);
            return Ok(new { message = "Kullanıcı oluşturuldu." });
        }
    }
}
