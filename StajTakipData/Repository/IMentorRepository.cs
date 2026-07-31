using StajTakip.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Data.Repository
{
    public interface IMentorRepository
    {
        Task<List<Mentor>> GetAllAsync();
        Task<Mentor?> GetByIdAsync(int id);
        Task AddAsync(Mentor mentor);
        Task UpdateAsync(Mentor mentor);
        Task<bool> DeleteAsync(int id);
    }
}
