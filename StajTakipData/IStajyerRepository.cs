using StajTakip.Core.Dto;
using StajTakip.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Data.Repository
{
    //kullanıcının hiç görmeyeceği, sadece verinin veritabanına nasıl gidip geleceğini belirleyen kod katmanlarındayız 
    public interface IStajyerRepository
    {
        //Bu bir "sözleşme" — "Stajyer verileriyle ilgili şu işlemleri yapabilirim" diye bir liste. Gerçek kodu değil, sadece hangi metotların olacağını tanımlıyoruz.
       // Bu sözleşmeyi gerçekten uygulayan(implement eden) bir sınıf yazacağız.

        Task<List<Stajyer>> GetAllAsync();
        Task<List<StajyerListDto>> GetAllWithDetailsAsync();
        Task<Stajyer?> GetByIdAsync(int id);
        Task AddAsync (Stajyer stajyer);
        Task UpdateAsync (Stajyer stajyer);
        Task DeleteAsync (int id);
    }
}
