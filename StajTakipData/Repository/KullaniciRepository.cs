using Dapper;
using StajTakip.Core.Dto;
using StajTakip.Data.Context;

namespace StajTakip.Data.Repository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly StajTakipDbContext _dbContext;

        public KullaniciRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<KullaniciAuthDto?> GetByKullaniciAdiAsync(string kullaniciAdi)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            return await connection.QueryFirstOrDefaultAsync<KullaniciAuthDto>(sql, new { KullaniciAdi = kullaniciAdi });
        }

        public async Task AddAsync(KullaniciAuthDto kullanici)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
        INSERT INTO dbo.Kullanicilar (KullaniciAdi, Sifre, AdSoyad)
        VALUES (@KullaniciAdi, @Sifre, @AdSoyad);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

            var newId = await connection.ExecuteScalarAsync<int>(sql, kullanici);
            kullanici.Id = newId;
        }

        /* ================== ESKİ (Entity tabanlı Dapper) KOD — yedek ==================
        using StajTakip.Core.Entity;

        public async Task<Kullanici?> GetByKullaniciAdiAsync(string kullaniciAdi)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            return await connection.QueryFirstOrDefaultAsync<Kullanici>(sql, new { KullaniciAdi = kullaniciAdi });
        }

        public async Task AddAsync(Kullanici kullanici)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
        INSERT INTO dbo.Kullanicilar (KullaniciAdi, Sifre, AdSoyad)
        VALUES (@KullaniciAdi, @Sifre, @AdSoyad);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

            var newId = await connection.ExecuteScalarAsync<int>(sql, kullanici);
            kullanici.Id = newId;
        }
        ================== ESKİ KOD SONU ================== */
    }
}