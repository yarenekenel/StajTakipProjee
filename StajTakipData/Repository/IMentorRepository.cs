using StajTakip.Core.Dto;

namespace StajTakip.Data.Repository
{
    public interface IMentorRepository
    {
        Task<List<MentorResponse>> GetAllAsync();
        Task<MentorResponse?> GetByIdAsync(int id);
        Task<MentorResponse> AddAsync(MentorRequest request);
        Task UpdateAsync(int id, MentorRequest request);
        Task<bool> DeleteAsync(int id);
    }

    /* ================== ESKİ (Entity tabanlı) INTERFACE — yedek ==================
    using StajTakip.Core.Entity;

    public interface IMentorRepository
    {
        Task<List<Mentor>> GetAllAsync();
        Task<Mentor?> GetByIdAsync(int id);
        Task AddAsync(Mentor mentor);
        Task UpdateAsync(Mentor mentor);
        Task<bool> DeleteAsync(int id);
    }
   */
}