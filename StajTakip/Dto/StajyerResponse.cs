using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Core.Dto
{
    //Kullanıcıya geri döndüreceğimiz bilgiler(bu sefer Id de var, çünkü artık kayıt oluşmuş, listelerken/gösterirken Id lazım)
    public class StajyerResponse
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Okul { get; set; }
        public string Bolum { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public bool AktifMi { get; set; }
        public int? KurumId { get; set; }
        public int? MentorId { get; set; }
    }
}
