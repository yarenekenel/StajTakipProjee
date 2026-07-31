using StajTakip.Core.Dto;

namespace StajTakip.Data.Repository
{
    public interface IStajyerRepository
    {
        Task<List<StajyerResponse>> GetAllAsync();
        Task<List<StajyerListDto>> GetAllWithDetailsAsync();
        Task<StajyerResponse?> GetByIdAsync(int id);
        Task<StajyerResponse> AddAsync(StajyerRequest request);
        Task UpdateAsync(int id, StajyerRequest request);
        Task DeleteAsync(int id);
    }

    /* ================== ESKİ (Entity tabanlı) INTERFACE — yedek ==================
    using StajTakip.Core.Entity;

    public interface IStajyerRepository
    {
        Task<List<Stajyer>> GetAllAsync();
        Task<List<StajyerListDto>> GetAllWithDetailsAsync();
        Task<Stajyer?> GetByIdAsync(int id);
        Task AddAsync (Stajyer stajyer);
        Task UpdateAsync (Stajyer stajyer);
        Task DeleteAsync (int id);
    }
     */
}