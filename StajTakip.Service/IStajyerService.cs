using StajTakip.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Service
{
    public interface IStajyerService
    {
        Task<List<StajyerResponse>> GetAllAsync();
        Task<List<StajyerListDto>> GetAllWithDetailsAsync();
        Task<StajyerResponse?> GetByIdAsync(int id);
        Task<StajyerResponse> AddAsync(StajyerRequest request);
        Task UpdateAsync(int id, StajyerRequest request);
        Task DeleteAsync(int id);
    }
}
