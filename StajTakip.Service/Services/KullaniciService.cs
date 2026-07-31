using StajTakip.Core.Dto;
using StajTakip.Core.Entity;
using StajTakip.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Service.Services
{
    public class KullaniciService : IKullaniciService
    {
        private readonly IKullaniciRepository _repository;

        public KullaniciService(IKullaniciRepository repository)
        {
            _repository = repository;
        }

        public async Task<KullaniciResponse?> LoginAsync(LoginRequest request)
        {
            var kullanici = await _repository.GetByKullaniciAdiAsync(request.KullaniciAdi);

            if (kullanici == null)
                return null; // kullanıcı adı bulunamadı

            bool sifreDogruMu = BCrypt.Net.BCrypt.Verify(request.Sifre, kullanici.Sifre);

            if (!sifreDogruMu)
                return null; // şifre yanlış

            return new KullaniciResponse
            {
                Id = kullanici.Id,
                KullaniciAdi = kullanici.KullaniciAdi,
                AdSoyad = kullanici.AdSoyad
            };
        }

        public async Task RegisterAsync(LoginRequest request)
        {
            var kullanici = new Kullanici
            {
                KullaniciAdi = request.KullaniciAdi,
                Sifre = BCrypt.Net.BCrypt.HashPassword(request.Sifre)
            };

            await _repository.AddAsync(kullanici);
        }
    }
}
