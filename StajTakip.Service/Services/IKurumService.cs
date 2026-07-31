using StajTakip.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Service.Services
{
    public interface IKurumService
    {
        Task<List<KurumResponse>> GetAllAsync();
        Task<KurumResponse?> GetByIdAsync(int id);
        Task<KurumResponse> AddAsync(KurumRequest request);
        Task UpdateAsync(int id, KurumRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
