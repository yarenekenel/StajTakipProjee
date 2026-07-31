using StajTakip.Core.Dto;
using StajTakip.Data.Repository;

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
            return await _repository.GetAllAsync();
        }

        public async Task<KurumResponse?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<KurumResponse> AddAsync(KurumRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task UpdateAsync(int id, KurumRequest request)
        {
            await _repository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        /* ================== ESKİ (Entity tabanlı) KOD — yedek ==================
        using StajTakip.Core.Entity;

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

        private static KurumResponse MapToResponse(Kurum k) => new KurumResponse
        {
            Id = k.Id,
            KurumAdi = k.KurumAdi,
            Adres = k.Adres,
            Telefon = k.Telefon,
            Sektor = k.Sektor
        };
        ================== ESKİ KOD SONU ================== */
    }
}