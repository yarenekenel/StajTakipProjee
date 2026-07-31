using StajTakip.Core.Dto;

namespace StajTakip.Data.Repository
{
    public interface IKullaniciRepository
    {
        Task<KullaniciAuthDto?> GetByKullaniciAdiAsync(string kullaniciAdi);
        Task AddAsync(KullaniciAuthDto kullanici);
    }
}

    /* ================== ESKİ (Entity tabanlı) INTERFACE — yedek ==================
    using StajTakip.Core.Entity;

    public interface IKullaniciRepository
    {
        Task<Kullanici?> GetByKullaniciAdiAsync(string kullaniciAdi);
        Task AddAsync(Kullanici kullanici);
    } */
  
