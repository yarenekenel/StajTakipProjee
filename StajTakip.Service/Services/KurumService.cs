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
    public class KurumService : IKurumService
    {
        private readonly IKurumRepository _repository;

        public KurumService(IKurumRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<KurumResponse>> GetAllAsync()
        {
            var kurumlar = await _repository.GetAllAsync();
            return kurumlar.Select(MapToResponse).ToList();
        }

        public async Task<KurumResponse?> GetByIdAsync(int id)
        {
            var kurum = await _repository.GetByIdAsync(id);
            if (kurum == null) return null;
            return MapToResponse(kurum);
        }

        public async Task<KurumResponse> AddAsync(KurumRequest request)
        {
            var kurum = new Kurum
            {
                KurumAdi = request.KurumAdi,
                Adres = request.Adres,
                Telefon = request.Telefon,
                Sektor = request.Sektor
            };

            await _repository.AddAsync(kurum);
            return MapToResponse(kurum);
        }

        public async Task UpdateAsync(int id, KurumRequest request)
        {
            var kurum = await _repository.GetByIdAsync(id);
            if (kurum == null) return;

            kurum.KurumAdi = request.KurumAdi;
            kurum.Adres = request.Adres;
            kurum.Telefon = request.Telefon;
            kurum.Sektor = request.Sektor;

            await _repository.UpdateAsync(kurum);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static KurumResponse MapToResponse(Kurum k) => new KurumResponse
        {
            Id = k.Id,
            KurumAdi = k.KurumAdi,
            Adres = k.Adres,
            Telefon = k.Telefon,
            Sektor = k.Sektor
        };
    }
}
