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
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _repository;

        public MentorService(IMentorRepository repository)
        {
            _repository = repository;
        }

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

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static MentorResponse MapToResponse(Mentor m) => new MentorResponse
        {
            Id = m.Id,
            Ad = m.Ad,
            Soyad = m.Soyad,
            Unvan = m.Unvan,
            KurumId = m.KurumId
        };
    }
}
