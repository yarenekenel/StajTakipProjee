using StajTakip.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Service.Services
{
    public interface IMentorService
    {
        Task<List<MentorResponse>> GetAllAsync();
        Task<MentorResponse?> GetByIdAsync(int id);
        Task<MentorResponse> AddAsync(MentorRequest request);
        Task UpdateAsync(int id, MentorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
