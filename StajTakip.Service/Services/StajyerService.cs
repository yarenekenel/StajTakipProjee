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
    public class StajyerService : IStajyerService
    {
        private readonly IStajyerRepository _repository;

        public StajyerService(IStajyerRepository repository)
        {
            _repository = repository;
        }
        public async Task<StajyerResponse> AddAsync(StajyerRequest request)
        {
            var stajyer = new Stajyer
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                DogumTarihi = request.DogumTarihi,
                Okul = request.Okul,
                Bolum = request.Bolum,
                BaslangicTarihi = request.BaslangicTarihi,
                BitisTarihi = request.BitisTarihi,
                AktifMi = true, // yeni eklenen stajyer varsayılan olarak aktif kabul edilir
                KurumId = request.KurumId,      
                MentorId = request.MentorId
            };

            await _repository.AddAsync(stajyer);

            return new StajyerResponse
            {
                Id = stajyer.Id,
                Ad = stajyer.Ad,
                Soyad = stajyer.Soyad,
                DogumTarihi = stajyer.DogumTarihi,
                Okul = stajyer.Okul,
                Bolum = stajyer.Bolum,
                BaslangicTarihi = stajyer.BaslangicTarihi,
                BitisTarihi = stajyer.BitisTarihi,
                AktifMi = stajyer.AktifMi,
                KurumId = stajyer.KurumId,      
                MentorId = stajyer.MentorId
            };

        }

        public async Task<List<StajyerResponse>> GetAllAsync()
        {
            var stajyerler = await _repository.GetAllAsync();

            return stajyerler.Select(s => new StajyerResponse
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

        }

        public async Task<List<StajyerListDto>> GetAllWithDetailsAsync()
        {
            return await _repository.GetAllWithDetailsAsync();
        }

        public async Task<StajyerResponse?> GetByIdAsync(int id)
        {
            var s = await _repository.GetByIdAsync(id);
            if (s == null) return null;

            return new StajyerResponse
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
            };
        }

        public async Task UpdateAsync(int id, StajyerRequest request)
        {
            var stajyer = await _repository.GetByIdAsync(id);
            if (stajyer == null) return;

            stajyer.Ad = request.Ad;
            stajyer.Soyad = request.Soyad;
            stajyer.DogumTarihi = request.DogumTarihi;
            stajyer.Okul = request.Okul;
            stajyer.Bolum = request.Bolum;
            stajyer.BaslangicTarihi = request.BaslangicTarihi;
            stajyer.BitisTarihi = request.BitisTarihi;
            stajyer.KurumId = request.KurumId;     
            stajyer.MentorId = request.MentorId;

            await _repository.UpdateAsync(stajyer);
        
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
