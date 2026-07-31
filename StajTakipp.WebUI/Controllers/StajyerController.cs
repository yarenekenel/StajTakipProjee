using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using StajTakipp.WebUI.Models;
using StajTakip.Core.Dto;
using StajTakip.Service;
using StajTakip.Service.Services;
using System.Text;
using System.Text.Json;

namespace StajTakipp.WebUI.Controllers
{
    [Authorize]
    public class StajyerController : Controller
    {
        private readonly IStajyerService _stajyerService;
        private readonly IKurumService _kurumService;
        private readonly IMentorService _mentorService;

        public StajyerController(IStajyerService stajyerService, IKurumService kurumService, IMentorService mentorService)
        {
            _stajyerService = stajyerService;
            _kurumService = kurumService;
            _mentorService = mentorService;
        }

        // Kurum ve Mentör listelerini Service üzerinden çekip ViewBag'e koyan yardımcı metod
        private async Task DoldurDropdownlar()
        {
            var kurumResponses = await _kurumService.GetAllAsync();
            var kurumlar = kurumResponses.Select(k => new KurumViewModel
            {
                Id = k.Id,
                KurumAdi = k.KurumAdi
            }).ToList();

            var mentorResponses = await _mentorService.GetAllAsync();
            var mentorler = mentorResponses.Select(m => new MentorViewModel
            {
                Id = m.Id,
                Ad = m.Ad,
                Soyad = m.Soyad,
                Unvan = m.Unvan,
                KurumId = m.KurumId
            }).ToList();

            ViewBag.Kurumlar = new SelectList(kurumlar, "Id", "KurumAdi");
            ViewBag.Mentorler = new SelectList(mentorler, "Id", "AdSoyad");
        }

        [HttpGet]
        public async Task<IActionResult> Ekle()
        {
            await DoldurDropdownlar();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(StajyerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await DoldurDropdownlar();
                return View(model);
            }

            var request = new StajyerRequest
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                DogumTarihi = model.DogumTarihi,
                Okul = model.Okul,
                Bolum = model.Bolum,
                BaslangicTarihi = model.BaslangicTarihi,
                BitisTarihi = model.BitisTarihi,
                AktifMi = model.AktifMi,
                KurumId = model.KurumId,
                MentorId = model.MentorId
            };

            try
            {
                await _stajyerService.AddAsync(request);
                TempData["BasariMesaji"] = "Stajyer basariyla kaydedildi.";
                return RedirectToAction("Listele");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await DoldurDropdownlar();
                return View(model);
            }
        }

        public IActionResult Basarili()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            var stajyerResponses = await _stajyerService.GetAllAsync();
            var stajyerler = stajyerResponses.Select(s => new StajyerViewModel
            {
                Id = s.Id,
                Ad = s.Ad,
                Soyad = s.Soyad,
                DogumTarihi = s.DogumTarihi,
                Okul = s.Okul,
                Bolum = s.Bolum,
                BaslangicTarihi = s.BaslangicTarihi,
                BitisTarihi = s.BitisTarihi,
                AktifMi = s.AktifMi,
                KurumId = s.KurumId,
                MentorId = s.MentorId
            }).ToList();

            var kurumResponses = await _kurumService.GetAllAsync();
            var kurumlar = kurumResponses.Select(k => new KurumViewModel
            {
                Id = k.Id,
                KurumAdi = k.KurumAdi
            }).ToList();
            ViewBag.Kurumlar = kurumlar;

            var mentorResponses = await _mentorService.GetAllAsync();
            var mentorler = mentorResponses.Select(m => new MentorViewModel
            {
                Id = m.Id,
                Ad = m.Ad,
                Soyad = m.Soyad,
                Unvan = m.Unvan,
                KurumId = m.KurumId
            }).ToList();
            ViewBag.Mentorler = mentorler;

            return View(stajyerler);
        }

        [HttpGet]
        public async Task<IActionResult> Guncelle(int id)
        {
            var stajyerResponse = await _stajyerService.GetByIdAsync(id);

            if (stajyerResponse == null)
            {
                return NotFound();
            }

            var stajyer = new StajyerViewModel
            {
                Id = stajyerResponse.Id,
                Ad = stajyerResponse.Ad,
                Soyad = stajyerResponse.Soyad,
                DogumTarihi = stajyerResponse.DogumTarihi,
                Okul = stajyerResponse.Okul,
                Bolum = stajyerResponse.Bolum,
                BaslangicTarihi = stajyerResponse.BaslangicTarihi,
                BitisTarihi = stajyerResponse.BitisTarihi,
                AktifMi = stajyerResponse.AktifMi,
                KurumId = stajyerResponse.KurumId,
                MentorId = stajyerResponse.MentorId
            };

            await DoldurDropdownlar();
            return View(stajyer);
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(StajyerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await DoldurDropdownlar();
                return View(model);
            }

            var request = new StajyerRequest
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                DogumTarihi = model.DogumTarihi,
                Okul = model.Okul,
                Bolum = model.Bolum,
                BaslangicTarihi = model.BaslangicTarihi,
                BitisTarihi = model.BitisTarihi,
                AktifMi = model.AktifMi,
                KurumId = model.KurumId,
                MentorId = model.MentorId
            };

            try
            {
                await _stajyerService.UpdateAsync(model.Id, request);
                return RedirectToAction("Listele");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Güncelleme yapılamadı");
                await DoldurDropdownlar();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Sil(int id)
        {
            await _stajyerService.DeleteAsync(id);
            return RedirectToAction("Listele");
        }
    }
}


/* ================== ESKİ KOD (HttpClient tabanlı, yedek) ==================

private readonly IHttpClientFactory _httpClientFactory;

public StajyerController(IHttpClientFactory httpClientFactory)
{
    _httpClientFactory = httpClientFactory;
}

private async Task DoldurDropdownlar()
{
    var client = _httpClientFactory.CreateClient("StajTakipApi");

    var kurumlar = new List<KurumViewModel>();
    var kurumResponse = await client.GetAsync("api/Kurum");
    if (kurumResponse.IsSuccessStatusCode)
    {
        var kurumJson = await kurumResponse.Content.ReadAsStringAsync();
        var deserializedKurum = JsonSerializer.Deserialize<List<KurumViewModel>>(kurumJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (deserializedKurum != null)
        {
            kurumlar = deserializedKurum;
        }
    }

    var mentorler = new List<MentorViewModel>();
    var mentorResponse = await client.GetAsync("api/Mentor");
    if (mentorResponse.IsSuccessStatusCode)
    {
        var mentorJson = await mentorResponse.Content.ReadAsStringAsync();
        var deserializedMentor = JsonSerializer.Deserialize<List<MentorViewModel>>(mentorJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (deserializedMentor != null)
        {
            mentorler = deserializedMentor;
        }
    }

    ViewBag.Kurumlar = new SelectList(kurumlar, "Id", "KurumAdi");
    ViewBag.Mentorler = new SelectList(mentorler, "Id", "AdSoyad");
}

[HttpGet]
public async Task<IActionResult> Ekle()
{
    await DoldurDropdownlar();
    return View();
}

[HttpPost]
public async Task<IActionResult> Ekle(StajyerViewModel model)
{
    if (!ModelState.IsValid)
    {
        await DoldurDropdownlar();
        return View(model);
    }

    var client = _httpClientFactory.CreateClient("StajTakipApi");
    var json = JsonSerializer.Serialize(model);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await client.PostAsync("api/Stajyer", content);

    if (response.IsSuccessStatusCode)
    {
        TempData["BasariMesaji"] = "Stajyer başarıyla kaydedildi.";
        return RedirectToAction("Listele");
    }

    var errorJson = await response.Content.ReadAsStringAsync();
    Dictionary<string, string>? errorObj = null;

    if (!string.IsNullOrEmpty(errorJson) && errorJson.TrimStart().StartsWith("{"))
    {
        try
        {
            errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorJson);
        }
        catch
        {
            errorObj = null;
        }
    }

    var errorMessage = (errorObj != null && errorObj.ContainsKey("message"))
        ? errorObj["message"]
        : (!string.IsNullOrEmpty(errorJson) ? errorJson : "Kayıt sırasında bir hata oluştu.");

    ModelState.AddModelError("", errorMessage);
    await DoldurDropdownlar();
    return View(model);
}

public IActionResult Basarili()
{
    return View();
}

[HttpGet]
public async Task<IActionResult> Listele()
{
    var client = _httpClientFactory.CreateClient("StajTakipApi");

    var response = await client.GetAsync("api/Stajyer");

    if (!response.IsSuccessStatusCode)
    {
        return View(new List<StajyerViewModel>());
    }

    var json = await response.Content.ReadAsStringAsync();

    var stajyerler = JsonSerializer.Deserialize<List<StajyerViewModel>>(json, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

    if (stajyerler == null)
    {
        stajyerler = new List<StajyerViewModel>();
    }

    var kurumlar = new List<KurumViewModel>();
    var kurumResponse = await client.GetAsync("api/Kurum");
    if (kurumResponse.IsSuccessStatusCode)
    {
        var kurumJson = await kurumResponse.Content.ReadAsStringAsync();
        var deserializedKurum = JsonSerializer.Deserialize<List<KurumViewModel>>(kurumJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (deserializedKurum != null)
        {
            kurumlar = deserializedKurum;
        }
    }
    ViewBag.Kurumlar = kurumlar;

    var mentorler = new List<MentorViewModel>();
    var mentorResponse = await client.GetAsync("api/Mentor");
    if (mentorResponse.IsSuccessStatusCode)
    {
        var mentorJson = await mentorResponse.Content.ReadAsStringAsync();
        var deserializedMentor = JsonSerializer.Deserialize<List<MentorViewModel>>(mentorJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (deserializedMentor != null)
        {
            mentorler = deserializedMentor;
        }
    }
    ViewBag.Mentorler = mentorler;

    return View(stajyerler);
}

[HttpGet]
public async Task<IActionResult> Guncelle(int id)
{
    var client = _httpClientFactory.CreateClient("StajTakipApi");

    var response = await client.GetAsync($"api/Stajyer/{id}");

    if (!response.IsSuccessStatusCode)
    {
        return NotFound();
    }

    var json = await response.Content.ReadAsStringAsync();

    var stajyer = JsonSerializer.Deserialize<StajyerViewModel>(json, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

    await DoldurDropdownlar();
    return View(stajyer);
}

[HttpPost]
public async Task<IActionResult> Guncelle(StajyerViewModel model)
{
    if (!ModelState.IsValid)
    {
        await DoldurDropdownlar();
        return View(model);
    }

    var client = _httpClientFactory.CreateClient("StajTakipApi");
    var json = JsonSerializer.Serialize(model);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await client.PutAsync($"api/Stajyer/{model.Id}", content);
    if (response.IsSuccessStatusCode)
    {
        return RedirectToAction("Listele");
    }
    ModelState.AddModelError("", "Güncelleme yapılamadı");
    await DoldurDropdownlar();
    return View(model);
}

[HttpGet]
public async Task<IActionResult> Sil(int id)
{
    var client = _httpClientFactory.CreateClient("StajTakipApi");

    var response = await client.DeleteAsync($"api/Stajyer/{id}");

    return RedirectToAction("Listele");
}
*/


//client.GetAsync("api/Stajyer") ile Api'ye "bana tüm stajyerleri ver" isteği göndermiştik. Api de bize bir cevap (response) döndürüyor — ama bu cevap ham haliyle direkt kullanılabilir değil, önce onun içeriğini (content) okumamız gerekiyor.

//response.Content.ReadAsStringAsync() diyerek, "bu cevabın içindeki veriyi, düz bir metin (string) olarak bana ver" diyoruz.Api zaten JSON formatında cevap verdiği için, bu metin aslında bir JSON metni olacak

//Deseralize-> bu JSON metnini al, tekrar gerçek bir C# nesnesine/listesine çevir" diyoruz.

//Api'den gelen JSON metnini oku → o metni gerçek bir C# stajyer listesine çevir, böylece View'da .Ad, .Soyad gibi alanlara erişebilelim

// GET: Kullanıcı "Düzenle" linkine tıkladığında, o kişinin mevcut bilgileriyle dolu bir form açılacak(Api'den GetById ile çekeceğiz)

// POST: Kullanıcı değişiklik yapıp "Güncelle"ye bastığında, yeni bilgiler Api'ye gönderilecek

//[HttpGet] Guncelle(int id) yazmıştık, şimdi[HttpPost] Guncelle(StajyerViewModel model) ekliyoruz — isimleri aynı ama parametreleri farklı.C#'ta buna "method overloading" (metot aşırı yükleme) denir — aynı isimde birden fazla metot olabilir, yeter ki parametreleri (tipleri/sayısı) farklı olsun.

//POST, yeni bir kayıt oluşturmak için kullanılır. Ama burada mevcut bir kaydı güncelliyoruz, yeni oluşturmuyoruz. HTTP standartlarında bu işlem için PUT kullanılır. Api Controller'ında da [HttpPut("{id}")] diye bir uç nokta yazmıştık — işte ona istek atıyoruz.

