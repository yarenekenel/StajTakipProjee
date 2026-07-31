using StajTakip.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Data.Repository
{
    public interface IKurumRepository
    {
        Task<List<Kurum>> GetAllAsync();
        Task<Kurum?> GetByIdAsync(int id);
        Task AddAsync(Kurum kurum);
        Task UpdateAsync(Kurum kurum);
        Task<bool> DeleteAsync(int id);
    }
}
