using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StajTakipp.WebUI.Models;
using System.Text;
using System.Text.Json;

namespace StajTakipp.WebUI.Controllers
{
    public class MentorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MentorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Ekle()
        {
            var client = _httpClientFactory.CreateClient("StajTakipApi");
            var response = await client.GetAsync("api/Kurum");
            var kurumlar = new List<KurumViewModel>();
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<KurumViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (deserialized != null)
                {
                    kurumlar = deserialized;
                }
            }
            ViewBag.Kurumlar = new SelectList(kurumlar, "Id", "KurumAdi");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(MentorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var client2 = _httpClientFactory.CreateClient("StajTakipApi");
                var response2 = await client2.GetAsync("api/Kurum");
                var kurumlar2 = new List<KurumViewModel>();
                if (response2.IsSuccessStatusCode)
                {
                    var json2 = await response2.Content.ReadAsStringAsync();
                    var deserialized2 = JsonSerializer.Deserialize<List<KurumViewModel>>(json2, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (deserialized2 != null)
                    {
                        kurumlar2 = deserialized2;
                    }
                }
                ViewBag.Kurumlar = new SelectList(kurumlar2, "Id", "KurumAdi");
                return View(model);
            }

            var client = _httpClientFactory.CreateClient("StajTakipApi");
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Mentor", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["BasariMesaji"] = "Mentor basariyla kaydedildi.";
                return RedirectToAction("Listele");
            }

            ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            var client = _httpClientFactory.CreateClient("StajTakipApi");

            // Mentörleri çek
            var response = await client.GetAsync("api/Mentor");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<MentorViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var mentorler = JsonSerializer.Deserialize<List<MentorViewModel>>
                (json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // Kurumları çek (kurum adını göstermek için)
            var kurumResponse = await client.GetAsync("api/Kurum");

            if (kurumResponse.IsSuccessStatusCode)
            {
                var kurumJson = await kurumResponse.Content.ReadAsStringAsync();
                var kurumlar = JsonSerializer.Deserialize<List<KurumViewModel>>
                    (kurumJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                ViewBag.Kurumlar = kurumlar;
            }
            else
            {
                ViewBag.Kurumlar = new List<KurumViewModel>();
            }

            return View(mentorler);
        }

        [HttpGet]
        public async Task<IActionResult> Sil(int id)
        {
            var client = _httpClientFactory.CreateClient("StajTakipApi");
            var response = await client.DeleteAsync($"api/Mentor/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["SilmeHatasi"] = "Bu mentöre bağlı stajyer(ler) olduğu için silinemedi. Önce stajyerlerin mentörünü değiştirin.";
            }

            return RedirectToAction("Listele");
        }
    }
}