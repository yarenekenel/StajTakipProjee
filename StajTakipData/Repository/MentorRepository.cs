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
    public class MentorRepository : IMentorRepository
    {

        private readonly StajTakipDbContext _dbContext;

        public MentorRepository(StajTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
    }
}
