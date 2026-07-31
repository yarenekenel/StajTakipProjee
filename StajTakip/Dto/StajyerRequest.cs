using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Core.Dto
{
   // Kullanıcıdan form ile alacağımız bilgiler(Id yok, çünkü yeni kayıtta henüz Id oluşmamış olur — veritabanı otomatik verir)
    public class StajyerRequest
    {
        public string Ad {  get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Okul {  get; set; }
        public string Bolum { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public bool AktifMi { get; set; }
        public int? KurumId { get; set; }
        public int? MentorId { get; set; }

    }
}
