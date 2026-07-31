using Microsoft.AspNetCore.Mvc;
using StajTakipp.WebUI.Models;
using System.Text;
using System.Text.Json;

namespace StajTakipp.WebUI.Controllers
{
        public class KurumController : Controller
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public KurumController(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            [HttpGet]
            public IActionResult Ekle()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Ekle(KurumViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var client = _httpClientFactory.CreateClient("StajTakipApi");
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Kurum", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["BasariMesaji"] = "Kurum basariyla kaydedildi.";
                return RedirectToAction("Listele");
            }

            ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu.");
                return View(model);
            }

            [HttpGet]
            public async Task<IActionResult> Listele()
            {
                var client = _httpClientFactory.CreateClient("StajTakipApi");
                var response = await client.GetAsync("api/Kurum");

                if (!response.IsSuccessStatusCode)
                {
                    return View(new List<KurumViewModel>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var kurumlar = JsonSerializer.Deserialize<List<KurumViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(kurumlar);
            }

            [HttpGet]
            public async Task<IActionResult> Sil(int id)
            {
                var client = _httpClientFactory.CreateClient("StajTakipApi");
                var response = await client.DeleteAsync($"api/Kurum/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    // Api'den dönen hata mesajını oku (örn. "bağlı stajyer var" mesajı)
                    var errorJson = await response.Content.ReadAsStringAsync();
                    TempData["SilmeHatasi"] = "Bu kuruma bağlı stajyer(ler) olduğu için silinemedi. Önce stajyerlerin kurumunu değiştirin.";
                }

                return RedirectToAction("Listele");
            }
        }
}
