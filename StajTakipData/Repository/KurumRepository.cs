using Microsoft.Data.SqlClient;
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
    public class KurumRepository : IKurumRepository
    {
        private readonly StajTakipDbContext _dbContext;

        public KurumRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
    }
}

