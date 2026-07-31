using StajTakip.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StajTakip.Service.Services
{
    public interface IKullaniciService
    {
        Task<KullaniciResponse?> LoginAsync(LoginRequest request);
        Task RegisterAsync(LoginRequest request);
    }
}
