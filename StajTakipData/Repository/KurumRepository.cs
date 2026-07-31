using Microsoft.Data.SqlClient;
using StajTakip.Core.Dto;
using StajTakip.Data.Context;
using Dapper;

namespace StajTakip.Data.Repository
{
    public class KurumRepository : IKurumRepository
    {
        private readonly StajTakipDbContext _dbContext;

        public KurumRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<KurumResponse>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Kurumlar";
            var result = await connection.QueryAsync<KurumResponse>(sql);
            return result.ToList();
        }

        public async Task<KurumResponse?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Kurumlar WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<KurumResponse>(sql, new { Id = id });
        }

        public async Task<KurumResponse> AddAsync(KurumRequest request)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Kurumlar (KurumAdi, Adres, Telefon, Sektor)
                VALUES (@KurumAdi, @Adres, @Telefon, @Sektor);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            var newId = await connection.ExecuteScalarAsync<int>(sql, request);

            return new KurumResponse
            {
                Id = newId,
                KurumAdi = request.KurumAdi,
                Adres = request.Adres,
                Telefon = request.Telefon,
                Sektor = request.Sektor
            };
        }

        public async Task UpdateAsync(int id, KurumRequest request)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                UPDATE dbo.Kurumlar SET
                    KurumAdi = @KurumAdi,
                    Adres = @Adres,
                    Telefon = @Telefon,
                    Sektor = @Sektor
                WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Id = id,
                request.KurumAdi,
                request.Adres,
                request.Telefon,
                request.Sektor
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "DELETE FROM dbo.Kurumlar WHERE Id = @Id";

            try
            {
                await connection.ExecuteAsync(sql, new { Id = id });
                return true;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return false;
            }
        }

        /* ================== ESKİ (Entity tabanlı Dapper) KOD — yedek ==================
        using StajTakip.Core.Entity;

        public async Task<List<Kurum>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Kurumlar";
            var result = await connection.QueryAsync<Kurum>(sql);
            return result.ToList();
        }

        public async Task<Kurum?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Kurumlar WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Kurum>(sql, new { Id = id });
        }

        public async Task AddAsync(Kurum kurum)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Kurumlar (KurumAdi, Adres, Telefon, Sektor)
                VALUES (@KurumAdi, @Adres, @Telefon, @Sektor);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            var newId = await connection.ExecuteScalarAsync<int>(sql, kurum);
            kurum.Id = newId;
        }

        public async Task UpdateAsync(Kurum kurum)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                UPDATE dbo.Kurumlar SET
                    KurumAdi = @KurumAdi,
                    Adres = @Adres,
                    Telefon = @Telefon,
                    Sektor = @Sektor
                WHERE Id = @Id";

            await connection.ExecuteAsync(sql, kurum);
        }
         */
    }
}