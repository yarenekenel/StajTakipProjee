using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StajTakip.Core.Dto;
using StajTakip.Service.Services;
using StajTakipp.WebUI.Models;
using System.Security.Claims;

namespace StajTakipp.WebUI.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IKullaniciService _service;

        public KullaniciController(IKullaniciService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new LoginRequest
            {
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre
            };

            var kullanici = await _service.LoginAsync(request);

            if (kullanici == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View(model);
            }

            HttpContext.Session.SetString("KullaniciAdi", kullanici.KullaniciAdi);
            HttpContext.Session.SetInt32("KullaniciId", kullanici.Id);
            HttpContext.Session.SetString("GirisZamani", DateTime.Now.ToString());

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.KullaniciAdi)
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Ekle", "Stajyer");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
    }
}

//========================= ESKİ: Api'ye HTTP isteği atarak çalışan hali (referans için saklanıyor) ======================

/*
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StajTakipp.WebUI.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
namespace StajTakipp.WebUI.Controllers
{
    public class KullaniciController : Controller
    {
            private readonly IHttpClientFactory _httpClientFactory;
        public KullaniciController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = _httpClientFactory.CreateClient("StajTakipApi");
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Kullanici/login", content);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View(model);
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            var kullanici = JsonSerializer.Deserialize<KullaniciResultModel>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            HttpContext.Session.SetString("KullaniciAdi", kullanici.KullaniciAdi);
            HttpContext.Session.SetInt32("KullaniciId", kullanici.Id);
            HttpContext.Session.SetString("GirisZamani", DateTime.Now.ToString());
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.KullaniciAdi)
        };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("CookieAuth", principal);
            return RedirectToAction("Ekle", "Stajyer");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        private class KullaniciResultModel
        {
            public int Id { get; set; }
            public string KullaniciAdi { get; set; }
            public string? AdSoyad { get; set; }
        }
    }
}
*/