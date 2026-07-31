using Microsoft.Data.SqlClient;
using StajTakip.Core.Dto;
using StajTakip.Data.Context;
using Dapper;

namespace StajTakip.Data.Repository
{
    public class MentorRepository : IMentorRepository
    {
        private readonly StajTakipDbContext _dbContext;

        public MentorRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MentorResponse>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Mentorler";
            var result = await connection.QueryAsync<MentorResponse>(sql);
            return result.ToList();
        }

        public async Task<MentorResponse?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Mentorler WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<MentorResponse>(sql, new { Id = id });
        }

        public async Task<MentorResponse> AddAsync(MentorRequest request)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Mentorler (Ad, Soyad, Unvan, KurumId)
                VALUES (@Ad, @Soyad, @Unvan, @KurumId);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            var newId = await connection.ExecuteScalarAsync<int>(sql, request);

            return new MentorResponse
            {
                Id = newId,
                Ad = request.Ad,
                Soyad = request.Soyad,
                Unvan = request.Unvan,
                KurumId = request.KurumId
            };
        }

        public async Task UpdateAsync(int id, MentorRequest request)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                UPDATE dbo.Mentorler SET
                    Ad = @Ad,
                    Soyad = @Soyad,
                    Unvan = @Unvan,
                    KurumId = @KurumId
                WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Id = id,
                request.Ad,
                request.Soyad,
                request.Unvan,
                request.KurumId
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "DELETE FROM dbo.Mentorler WHERE Id = @Id";

            try
            {
                await connection.ExecuteAsync(sql, new { Id = id });
                return true;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return false; // bağlı stajyer olduğu için silinemedi
            }
        }

        /* ================== ESKİ (Entity tabanlı Dapper) KOD — yedek ==================
        using StajTakip.Core.Entity;

        public async Task<List<Mentor>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Mentorler";
            var result = await connection.QueryAsync<Mentor>(sql);
            return result.ToList();
        }

        public async Task<Mentor?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Mentorler WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Mentor>(sql, new { Id = id });
        }

        public async Task AddAsync(Mentor mentor)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Mentorler (Ad, Soyad, Unvan, KurumId)
                VALUES (@Ad, @Soyad, @Unvan, @KurumId);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            var newId = await connection.ExecuteScalarAsync<int>(sql, mentor);
            mentor.Id = newId;
        }

        public async Task UpdateAsync(Mentor mentor)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                UPDATE dbo.Mentorler SET
                    Ad = @Ad,
                    Soyad = @Soyad,
                    Unvan = @Unvan,
                    KurumId = @KurumId
                WHERE Id = @Id";

            await connection.ExecuteAsync(sql, mentor);
        }
        ================== ESKİ KOD SONU ================== */
    }
}