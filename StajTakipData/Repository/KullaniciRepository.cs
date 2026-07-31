using StajTakip.Core.Entity;
using StajTakip.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace StajTakip.Data.Repository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly StajTakipDbContext _dbContext;

        public KullaniciRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
    }
}
