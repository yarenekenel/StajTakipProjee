using StajTakip.Core.Dto;
using StajTakip.Data.Repository;

namespace StajTakip.Service.Services
{
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _repository;

        public MentorService(IMentorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MentorResponse>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MentorResponse?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MentorResponse> AddAsync(MentorRequest request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task UpdateAsync(int id, MentorRequest request)
        {
            await _repository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        /* ================== ESKİ (Entity tabanlı) KOD — yedek ==================
        using StajTakip.Core.Entity;

        public async Task<List<MentorResponse>> GetAllAsync()
        {
            var mentorler = await _repository.GetAllAsync();
            return mentorler.Select(MapToResponse).ToList();
        }

        public async Task<MentorResponse?> GetByIdAsync(int id)
        {
            var mentor = await _repository.GetByIdAsync(id);
            if (mentor == null) return null;
            return MapToResponse(mentor);
        }

        public async Task<MentorResponse> AddAsync(MentorRequest request)
        {
            var mentor = new Mentor
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                Unvan = request.Unvan,
                KurumId = request.KurumId
            };
            await _repository.AddAsync(mentor);
            return MapToResponse(mentor);
        }

        public async Task UpdateAsync(int id, MentorRequest request)
        {
            var mentor = await _repository.GetByIdAsync(id);
            if (mentor == null) return;
            mentor.Ad = request.Ad;
            mentor.Soyad = request.Soyad;
            mentor.Unvan = request.Unvan;
            mentor.KurumId = request.KurumId;
            await _repository.UpdateAsync(mentor);
        }

        private static MentorResponse MapToResponse(Mentor m) => new MentorResponse
        {
            Id = m.Id,
            Ad = m.Ad,
            Soyad = m.Soyad,
            Unvan = m.Unvan,
            KurumId = m.KurumId
        };
        ================== ESKİ KOD SONU ================== */
    }
}