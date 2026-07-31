using StajTakip.Core.Dto;

namespace StajTakip.Data.Repository
{
    public interface IKurumRepository
    {
        Task<List<KurumResponse>> GetAllAsync();
        Task<KurumResponse?> GetByIdAsync(int id);
        Task<KurumResponse> AddAsync(KurumRequest request);
        Task UpdateAsync(int id, KurumRequest request);
        Task<bool> DeleteAsync(int id);
    }

    /* ================== ESKİ (Entity tabanlı) INTERFACE — yedek ==================
    using StajTakip.Core.Entity;

    public interface IKurumRepository
    {
        Task<List<Kurum>> GetAllAsync();
        Task<Kurum?> GetByIdAsync(int id);
        Task AddAsync(Kurum kurum);
        Task UpdateAsync(Kurum kurum);
        Task<bool> DeleteAsync(int id);
    }
 */
}