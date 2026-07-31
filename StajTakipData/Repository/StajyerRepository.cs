using Dapper;
using StajTakip.Core.Dto;
using StajTakip.Data.Context;

namespace StajTakip.Data.Repository
{
    public class StajyerRepository : IStajyerRepository
    {
        private readonly StajTakipDbContext _dbContext;

        public StajyerRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StajyerResponse>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();

            const string sql = "SELECT * FROM dbo.Stajyerler";

            var result = await connection.QueryAsync<StajyerResponse>(sql);
            return result.ToList();
        }

        public async Task<List<StajyerListDto>> GetAllWithDetailsAsync()
        {
            using var connection = _dbContext.CreateConnection();

            const string sql = @"
        SELECT 
            s.Id, s.Ad, s.Soyad, s.DogumTarihi, s.Okul, s.Bolum,
            s.BaslangicTarihi, s.BitisTarihi, s.AktifMi,
            s.KurumId, k.KurumAdi,
            s.MentorId, m.Ad AS MentorAd, m.Soyad AS MentorSoyad
        FROM dbo.Stajyerler s
        LEFT JOIN dbo.Kurumlar k ON s.KurumId = k.Id
        LEFT JOIN dbo.Mentorler m ON s.MentorId = m.Id";

            var result = await connection.QueryAsync<StajyerListDto>(sql);
            return result.ToList();
        }

        public async Task<StajyerResponse?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();

            const string sql = "SELECT * FROM dbo.Stajyerler WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<StajyerResponse>(sql, new { Id = id });
        }

        public async Task<StajyerResponse> AddAsync(StajyerRequest request)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
        INSERT INTO dbo.Stajyerler (Ad, Soyad, DogumTarihi, Okul, Bolum, BaslangicTarihi, BitisTarihi, AktifMi, KurumId, MentorId)
        VALUES (@Ad, @Soyad, @DogumTarihi, @Okul, @Bolum, @BaslangicTarihi, @BitisTarihi, @AktifMi, @KurumId, @MentorId);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

            int newId;
            try
            {
                newId = await connection.ExecuteScalarAsync<int>(sql, request);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new InvalidOperationException("Bu bilgilere (Ad, Soyad, Doğum Tarihi) sahip bir stajyer zaten kayıtlı.");
            }

            return new StajyerResponse
            {
                Id = newId,
                Ad = request.Ad,
                Soyad = request.Soyad,
                DogumTarihi = request.DogumTarihi,
                Okul = request.Okul,
                Bolum = request.Bolum,
                BaslangicTarihi = request.BaslangicTarihi,
                BitisTarihi = request.BitisTarihi,
                AktifMi = request.AktifMi,
                KurumId = request.KurumId,
                MentorId = request.MentorId
            };
        }

        public async Task UpdateAsync(int id, StajyerRequest request)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
        UPDATE dbo.Stajyerler SET
            Ad = @Ad,
            Soyad = @Soyad,
            DogumTarihi = @DogumTarihi,
            Okul = @Okul,
            Bolum = @Bolum,
            BaslangicTarihi = @BaslangicTarihi,
            BitisTarihi = @BitisTarihi,
            AktifMi = @AktifMi,
            KurumId = @KurumId,
            MentorId = @MentorId
        WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Id = id,
                request.Ad,
                request.Soyad,
                request.DogumTarihi,
                request.Okul,
                request.Bolum,
                request.BaslangicTarihi,
                request.BitisTarihi,
                request.AktifMi,
                request.KurumId,
                request.MentorId
            });
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();

            const string sql = "DELETE FROM dbo.Stajyerler WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new { Id = id });
        }

        /* ================== ESKİ (Entity tabanlı Dapper) KOD — yedek ==================
        using StajTakip.Core.Entity;

        public async Task<List<Stajyer>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Stajyerler";
            var result = await connection.QueryAsync<Stajyer>(sql);
            return result.ToList();
        }

        public async Task<Stajyer?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = "SELECT * FROM dbo.Stajyerler WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Stajyer>(sql, new { Id = id });
        }

        public async Task AddAsync(Stajyer stajyer)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
        INSERT INTO dbo.Stajyerler (Ad, Soyad, DogumTarihi, Okul, Bolum, BaslangicTarihi, BitisTarihi, AktifMi, KurumId, MentorId)
        VALUES (@Ad, @Soyad, @DogumTarihi, @Okul, @Bolum, @BaslangicTarihi, @BitisTarihi, @AktifMi, @KurumId, @MentorId);
        SELECT CAST(SCOPE_IDENTITY() AS int);";
            try
            {
                var newId = await connection.ExecuteScalarAsync<int>(sql, stajyer);
                stajyer.Id = newId;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new InvalidOperationException("Bu bilgilere (Ad, Soyad, Doğum Tarihi) sahip bir stajyer zaten kayıtlı.");
            }
        }

        public async Task UpdateAsync(Stajyer stajyer)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
        UPDATE dbo.Stajyerler SET
            Ad = @Ad, Soyad = @Soyad, DogumTarihi = @DogumTarihi, Okul = @Okul, Bolum = @Bolum,
            BaslangicTarihi = @BaslangicTarihi, BitisTarihi = @BitisTarihi, AktifMi = @AktifMi,
            KurumId = @KurumId, MentorId = @MentorId
        WHERE Id = @Id";
            await connection.ExecuteAsync(sql, stajyer);
        }
        */
    }
}