using StajTakip.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Data.Repository
{
    public interface IKullaniciRepository
    {
        Task<Kullanici?> GetByKullaniciAdiAsync(string kullaniciAdi);
        Task AddAsync(Kullanici kullanici);
    }
}
